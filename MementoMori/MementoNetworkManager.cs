using MementoMori.Exceptions;
using MementoMori.Extensions;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ortega.Common.Manager;

namespace MementoMori;

public class MementoNetworkManager
{
    private const string GameOs = "Android";

    private readonly MeMoriHttpClientHandler _meMoriHttpClientHandler;
    private readonly HttpClient _httpClient;
    private readonly HttpClient _unityHttpClient;

    private LoginRequest _lastLoginRequest;


    public string OrtegaAccessToken => _meMoriHttpClientHandler.OrtegaAccessToken;
    public string OrtegaMasterVersion => _meMoriHttpClientHandler.OrtegaMasterVersion;
    public string OrtegaAssetVersion => _meMoriHttpClientHandler.OrtegaAssetVersion;


    private Uri _apiAuth = new("https://prd1-auth.mememori-boi.com/api/");

    private Uri _apiHost;

    public string AssetCatalogUriFormat { get; private set; }
    public string AssetCatalogFixedUriFormat { get; private set; }
    public string MasterUriFormat { get; private set; }
    public string NoticeBannerImageUriFormat { get; private set; }
    public AppAssetVersionInfo AppAssetVersionInfo { get; private set; }

    private readonly ILogger<MementoNetworkManager> _logger;

    public MementoNetworkManager(ILogger<MementoNetworkManager> logger)
    {
        _logger = logger;

        _meMoriHttpClientHandler = new MeMoriHttpClientHandler();
        _httpClient = new HttpClient(_meMoriHttpClientHandler) {Timeout = TimeSpan.FromSeconds(10)};

        var response = GetResponse<GetDataUriRequest, GetDataUriResponse>(new GetDataUriRequest() {CountryCode = "CN"}).ConfigureAwait(false).GetAwaiter().GetResult();
        AssetCatalogUriFormat = response.AssetCatalogUriFormat;
        AssetCatalogFixedUriFormat = response.AssetCatalogFixedUriFormat;
        MasterUriFormat = response.MasterUriFormat;
        NoticeBannerImageUriFormat = response.NoticeBannerImageUriFormat;
        AppAssetVersionInfo = response.AppAssetVersionInfo;
        _meMoriHttpClientHandler.AppVersion = AppAssetVersionInfo.Version;

        _unityHttpClient = new HttpClient() {Timeout = TimeSpan.FromSeconds(30)};
        _unityHttpClient.DefaultRequestHeaders.Add("User-Agent", "UnityPlayer/2021.3.10f1 (UnityWebRequest/1.0, libcurl/7.80.0-DEV)");
        _unityHttpClient.DefaultRequestHeaders.Add("X-Unity-Version", "2021.3.10f1");
    }


    public async Task DownloadMasterCatalog()
    {
        _logger.LogInformation("下载 master 目录中...");
        var dataUriResponse = await GetResponse<GetDataUriRequest, GetDataUriResponse>(new GetDataUriRequest() {CountryCode = "CN", UserId = 0});

        var url = string.Format(dataUriResponse.MasterUriFormat, _meMoriHttpClientHandler.OrtegaMasterVersion, "master-catalog");
        var bytes = await _unityHttpClient.GetByteArrayAsync(url);
        var masterBookCatalog = MessagePackSerializer.Deserialize<MasterBookCatalog>(bytes);
        Directory.CreateDirectory("./Master");
        foreach (var (name, info) in masterBookCatalog.MasterBookInfoMap)
        {
            var localPath = $"./Master/{name}";
            if (File.Exists(localPath))
            {
                var md5 = await CalcFileMd5(localPath);
                if (md5 == info.Hash) continue;
                File.Delete(localPath);
            }

            var mbUrl = string.Format(dataUriResponse.MasterUriFormat, _meMoriHttpClientHandler.OrtegaMasterVersion, name);
            var fileBytes = await _unityHttpClient.GetByteArrayAsync(mbUrl);
            await File.WriteAllBytesAsync(localPath, fileBytes);
        }

        Masters.TextResourceTable.SetLanguageType(LanguageType.zhTW);
        Masters.LoadAllMasters();
        _logger.LogInformation("下载 master 目录完成");
    }

    public async Task DownloadAssets(CancellationToken cancellationToken)
    {
        _logger.LogInformation("下载 asset 目录中...");
        var name = $"{GameOs}/{_meMoriHttpClientHandler.OrtegaAssetVersion}.json";
        var assetCatalogUrl = string.Format(AssetCatalogFixedUriFormat, name);

        var content = await _unityHttpClient.GetStringAsync(assetCatalogUrl, cancellationToken);
        var jObject = JObject.Parse(content);
        var internalIds = jObject["m_InternalIds"]?.ToObject<string[]>();
        if (internalIds == null)
        {
            _logger.LogInformation("下载 asset 目录失败, 未解析到 m_InternalIds");
            return;
        }

        Directory.CreateDirectory("./Assets");
        await Parallel.ForEachAsync(internalIds, cancellationToken, async (internalId, token) =>
        {
            if (cancellationToken.IsCancellationRequested) return;
            if (!internalId.StartsWith("0#/")) return;

            var bundleId = internalId.Substring(3);
            var localPath = $"./Assets/{bundleId}";
            if (File.Exists(localPath)) return;

            var bundleUrl = string.Format(AssetCatalogFixedUriFormat, $"{GameOs}/{bundleId}");
            _logger.LogInformation($"下载 {bundleUrl}");
            var bytes = await ExecWithRetry(async () => await _unityHttpClient.GetByteArrayAsync(bundleUrl, cancellationToken));
            await File.WriteAllBytesAsync(localPath, bytes, cancellationToken);
        });

        _logger.LogInformation("下载 asset 目录完成");
    }

    private static async Task<T> ExecWithRetry<T>(Func<Task<T>> func, int retryCount = 10)
    {
        while (true)
            try
            {
                return await func();
            }
            catch
            {
                retryCount--;
                if (retryCount <= 0) throw;
                await Task.Delay(1000);
            }
    }

    private async Task<string> CalcFileMd5(string path)
    {
        byte[] retVal;
        using (var file = new FileStream(path, FileMode.Open))
        {
            var md5 = MD5.Create();
            retVal = await md5.ComputeHashAsync(file);
            file.Close();
        }

        var sb = new StringBuilder();
        foreach (var t in retVal) sb.Append(t.ToString("x2"));

        return sb.ToString();
    }

    public async Task<UserSyncData> Login(LoginRequest loginRequest, Action<string> log = null)
    {
        _lastLoginRequest = loginRequest;
        var authLoginResp = await GetResponse<LoginRequest, LoginResponse>(loginRequest, log);
        var playerDataInfo = authLoginResp.PlayerDataInfoList.MaxBy(d => d.LastLoginTime);
        if (playerDataInfo == null) throw new Exception("playerDataInfo is null");

        // get server host
        var resp = await GetResponse<GetServerHostRequest, GetServerHostResponse>(new GetServerHostRequest() {WorldId = playerDataInfo.WorldId}, log);
        _apiHost = new Uri(resp.ApiHost);

        // do login
        var loginPlayerResp = await GetResponse<LoginPlayerRequest, LoginPlayerResponse>(new LoginPlayerRequest
        {
            PlayerId = playerDataInfo.PlayerId, Password = playerDataInfo.Password
        }, log);
        return loginPlayerResp.UserSyncData;
    }


    public async Task<TResp> GetResponse<TReq, TResp>(TReq req, Action<string> log = null, Action<UserSyncData> userData = null)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase
    {
        log ??= Console.WriteLine;
        var authAttr = typeof(TReq).GetCustomAttribute<OrtegaAuthAttribute>();
        var apiAttr = typeof(TReq).GetCustomAttribute<OrtegaApiAttribute>();
        Uri uri;
        if (authAttr != null)
            uri = new Uri(_apiAuth, authAttr.Uri);
        else if (apiAttr != null)
            uri = new Uri(_apiHost, apiAttr.Uri);
        else
            throw new NotSupportedException();

        var bytes = MessagePackSerializer.Serialize(req);
        var respMsg = await _httpClient.PostAsync(uri, new ByteArrayContent(bytes) {Headers = {{"content-type", "application/json"}}});
        if (!respMsg.IsSuccessStatusCode) throw new InvalidOperationException(respMsg.ToString());

        var respBytes = await respMsg.Content.ReadAsByteArrayAsync();
        if (respMsg.Headers.TryGetValues("ortegastatuscode", out var headers2))
        {
            var ortegastatuscode = headers2.FirstOrDefault() ?? "";
            if (ortegastatuscode != "0")
            {
                var apiErrResponse = MessagePackSerializer.Deserialize<ApiErrorResponse>(respBytes);

                if (apiErrResponse.ErrorCode == ErrorCode.InvalidRequestHeader)
                {
                    log("登录失效, 正在重新登录");
                    await Login(_lastLoginRequest);
                }

                if (apiErrResponse.ErrorCode == ErrorCode.AuthLoginInvalidRequest)
                {
                    log("登录失败, 请检查帐号配置");
                }

                var errorCodeMessage = Masters.TextResourceTable.GetErrorCodeMessage(apiErrResponse.ErrorCode);
                log(uri.ToString());
                log($"{errorCodeMessage}");
                log(req.ToJson());
                log(apiErrResponse.ToJson());
                throw new ApiErrorException(apiErrResponse.ErrorCode);
            }
        }

        var response = MessagePackSerializer.Deserialize<TResp>(respBytes);
        if (response is IUserSyncApiResponse userSyncApiResponse) userData?.Invoke(userSyncApiResponse.UserSyncData);

        return response;
    }
}