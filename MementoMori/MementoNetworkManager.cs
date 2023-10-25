using MementoMori.Exceptions;
using MementoMori.Extensions;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using MementoMori.Common.Localization;
using MementoMori.Option;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ortega.Common.Manager;
using Microsoft.AspNetCore.Authentication.OAuth;

namespace MementoMori;

public class MementoNetworkManager
{
    private const string GameOs = "Android";

    private readonly MeMoriHttpClientHandler _meMoriHttpClientHandler;
    private readonly HttpClient _httpClient;
    private readonly HttpClient _unityHttpClient;
    public TimeManager TimeManager { get; } = new();

    private LoginRequest _lastLoginRequest;

    public CultureInfo CultureInfo { get; private set; } = new("zh-CN");
    public LanguageType LanguageType => parseLanguageType(CultureInfo);


    private Uri _apiAuth = new("https://prd1-auth.mememori-boi.com/api/");
    // private Uri _apiAuth = new("https://stg1-auth.mememori-boi.com/api/");

    private Uri _apiHost;

    public static string AssetCatalogUriFormat { get; private set; }
    public static string AssetCatalogFixedUriFormat { get; private set; }
    public static string MasterUriFormat { get; private set; }
    public static string NoticeBannerImageUriFormat { get; private set; }
    public static AppAssetVersionInfo AppAssetVersionInfo { get; private set; }

    private readonly ILogger<MementoNetworkManager> _logger;
    private IWritableOptions<AuthOption> _authOption;
    private static bool initialized;

    public MementoNetworkManager(ILogger<MementoNetworkManager> logger, IWritableOptions<AuthOption> authOption)
    {
        _logger = logger;
        _authOption = authOption;

        _meMoriHttpClientHandler = new MeMoriHttpClientHandler();
        _httpClient = new HttpClient(_meMoriHttpClientHandler);
        if (!Debugger.IsAttached) _httpClient.Timeout = TimeSpan.FromSeconds(10);

        if (!initialized)
        {
            var response = GetResponse<GetDataUriRequest, GetDataUriResponse>(new GetDataUriRequest() { CountryCode = "CN" }).ConfigureAwait(false).GetAwaiter().GetResult();
            AssetCatalogUriFormat = response.AssetCatalogUriFormat;
            AssetCatalogFixedUriFormat = response.AssetCatalogFixedUriFormat;
            MasterUriFormat = response.MasterUriFormat;
            NoticeBannerImageUriFormat = response.NoticeBannerImageUriFormat;
            AppAssetVersionInfo = response.AppAssetVersionInfo;
            _authOption.Update(x => x.AppVersion = AppAssetVersionInfo.Version);
            initialized = true;
        }
        _meMoriHttpClientHandler.AppVersion = AppAssetVersionInfo.Version;

        _unityHttpClient = new HttpClient();
        if (!Debugger.IsAttached) _unityHttpClient.Timeout = TimeSpan.FromSeconds(30);
        _unityHttpClient.DefaultRequestHeaders.Add("User-Agent", "UnityPlayer/2021.3.10f1 (UnityWebRequest/1.0, libcurl/7.80.0-DEV)");
        _unityHttpClient.DefaultRequestHeaders.Add("X-Unity-Version", "2021.3.10f1");
    }


    public async Task DownloadMasterCatalog()
    {
        _logger.LogInformation(ResourceStrings.Downloading_master_directory___);
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

        _logger.LogInformation(ResourceStrings.Download_master_directory_completed);
    }

    public void SetCultureInfo(CultureInfo cultureInfo)
    {
        Masters.TextResourceTable.SetLanguageType(parseLanguageType(cultureInfo));
        Masters.LoadAllMasters();
    }

    private LanguageType parseLanguageType(CultureInfo cultureInfo)
    {
        return cultureInfo.TwoLetterISOLanguageName switch
        {
            "zh" => LanguageType.zhTW,
            "en" => LanguageType.enUS,
            "ja" => LanguageType.jaJP,
            "ko" => LanguageType.koKR,
            "fr" => LanguageType.frFR,
            "de" => LanguageType.deDE,
            "es" => LanguageType.esMX,
            "pt" => LanguageType.ptBR,
            "th" => LanguageType.thTH,
            "id" => LanguageType.idID,
            "vi" => LanguageType.viVN,
            "ru" => LanguageType.ruRU,
            "ar" => LanguageType.arEG,
            _ => LanguageType.enUS
        };
    }

    public async Task DownloadAssets(string gameOs, string assetsPath, string assetsTmpPath, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Downloading asset catalog...");
        var name = $"{gameOs}/{_meMoriHttpClientHandler.OrtegaAssetVersion}.json";
        var assetCatalogUrl = string.Format(AssetCatalogFixedUriFormat, name);
        _logger.LogInformation($"download {assetCatalogUrl}");

        var content = await _unityHttpClient.GetStringAsync(assetCatalogUrl, cancellationToken);
        var jObject = JObject.Parse(content);
        var internalIds = jObject["m_InternalIds"]?.ToObject<string[]>();
        if (internalIds == null)
        {
            _logger.LogInformation("Download asset catalog failed, unable to retrieve m_InternalIds");
            return;
        }

        Directory.CreateDirectory(assetsPath);
        Directory.CreateDirectory(assetsTmpPath);
        _logger.LogInformation("Downloading assets...");
        await Parallel.ForEachAsync(internalIds, cancellationToken, async (internalId, token) =>
        {
            if (cancellationToken.IsCancellationRequested) return;
            if (!internalId.StartsWith("0#/")) return;

            var bundleId = internalId.Substring(3);
            var localPath = Path.Combine(assetsPath, bundleId);
            if (File.Exists(localPath)) return;

            var bundleUrl = string.Format(AssetCatalogFixedUriFormat, $"{GameOs}/{bundleId}");
            _logger.LogInformation($"download {bundleUrl}");
            var bytes = await ExecWithRetry(async () => await _unityHttpClient.GetByteArrayAsync(bundleUrl, cancellationToken));
            var localTmpPath = Path.Combine(assetsTmpPath, bundleId);
            await File.WriteAllBytesAsync(localTmpPath, bytes, cancellationToken);
        });

        _logger.LogInformation("Download assets finished");
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

    public async Task<List<PlayerDataInfo>> GetPlayerDataInfoList(LoginRequest loginRequest, Action<string> log = null)
    {
        _lastLoginRequest = loginRequest;
        var authLoginResp = await GetResponse<LoginRequest, LoginResponse>(loginRequest, log);
        return authLoginResp.PlayerDataInfoList;
    }

    public async Task Login(long worldId, Action<string> log = null)
    {
        var authLoginResp = await GetResponse<LoginRequest, LoginResponse>(_lastLoginRequest, log);
        var playerDataInfo = authLoginResp.PlayerDataInfoList.First(x => x.WorldId == worldId);

        var timeServerId = playerDataInfo.WorldId / 1000;
        var timeServerMb = Masters.TimeServerTable.GetById(timeServerId);
        TimeManager.SetTimeServerMb(timeServerMb);

        // get server host
        var resp = await GetResponse<GetServerHostRequest, GetServerHostResponse>(new GetServerHostRequest() {WorldId = playerDataInfo.WorldId}, log);
        _apiHost = new Uri(resp.ApiHost);

        // do login
        var loginPlayerResp = await GetResponse<LoginPlayerRequest, LoginPlayerResponse>(new LoginPlayerRequest
        {
            PlayerId = playerDataInfo.PlayerId, Password = playerDataInfo.Password
        }, log);
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
            uri = new Uri(_apiHost ?? throw new InvalidOperationException(ResourceStrings.PleaseLogin), apiAttr.Uri);
        else
            throw new NotSupportedException();

        var bytes = MessagePackSerializer.Serialize(req);
        var respMsg = await _httpClient.PostAsync(uri, new ByteArrayContent(bytes) {Headers = {{"content-type", "application/json; charset=UTF-8"}}});
        if (!respMsg.IsSuccessStatusCode) throw new InvalidOperationException(respMsg.ToString());

        var respBytes = await respMsg.Content.ReadAsByteArrayAsync();
        if (respMsg.Headers.TryGetValues("ortegastatuscode", out var headers2))
        {
            var ortegastatuscode = headers2.FirstOrDefault() ?? "";
            if (ortegastatuscode != "0")
            {
                var apiErrResponse = MessagePackSerializer.Deserialize<ApiErrorResponse>(respBytes);

                if (apiErrResponse.ErrorCode == ErrorCode.InvalidRequestHeader) log(ResourceStrings.Login_expired__please_log_in_again);

                if (apiErrResponse.ErrorCode == ErrorCode.AuthLoginInvalidRequest) log(ResourceStrings.Login_failed__please_check_your_account_configuration);

                if (apiErrResponse.ErrorCode == ErrorCode.CommonNoSession) log(Masters.TextResourceTable.GetErrorCodeMessage(ErrorCode.CommonNoSession));

                var errorCodeMessage = Masters.TextResourceTable.GetErrorCodeMessage(apiErrResponse.ErrorCode);
                log(uri.ToString());
                log($"{errorCodeMessage}");
                log(req.ToJson());
                log(apiErrResponse.ToJson());
                throw new ApiErrorException(apiErrResponse.ErrorCode);
            }
        }

        var response = MessagePackSerializer.Deserialize<TResp>(respBytes);
        // if (Debugger.IsAttached) log(response.ToJson());
        if (response is IUserSyncApiResponse userSyncApiResponse) userData?.Invoke(userSyncApiResponse.UserSyncData);

        return response;
    }
}