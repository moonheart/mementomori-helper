using MementoMori.Exceptions;
using MementoMori.Extensions;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share;
using MessagePack;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using AutoCtor;
using Grpc.Net.Client;
using Injectio.Attributes;
using MementoMori.Common.Localization;
using MementoMori.MagicOnion;
using MementoMori.Option;
using MementoMori.Ortega.Network.MagicOnion.Client;
using Newtonsoft.Json.Linq;

namespace MementoMori;

[RegisterTransient<MementoNetworkManager>]
[AutoConstruct]
public partial class MementoNetworkManager : IDisposable
{
    private const string GameOs = "Android";

    private MeMoriHttpClientHandler _meMoriHttpClientHandler = null;
    private HttpClient _httpClient;
    private HttpClient _unityHttpClient = null;
    public TimeManager TimeManager { get; } = new();

    private LoginRequest _lastLoginRequest;

    public long UserId { get; set; }
    public long PlayerId { get; set; }
    public CultureInfo CultureInfo { get; private set; } = new("zh-CN");
    public LanguageType LanguageType => parseLanguageType(CultureInfo);


    private Uri _apiAuth;
    private Uri _apiHost;
    private GrpcChannel _grpcChannel;
    private string AuthTokenOfMagicOnion;
    CancellationTokenSource cts = new();

    public static string AssetCatalogUriFormat { get; private set; }
    public static string AssetCatalogFixedUriFormat { get; private set; }
    public static string MasterUriFormat { get; private set; }
    public static string NoticeBannerImageUriFormat { get; private set; }
    public static AppAssetVersionInfo AppAssetVersionInfo { get; private set; }

    public MeMoriHttpClientHandler MoriHttpClientHandler => _meMoriHttpClientHandler;

    private readonly ILogger<MementoNetworkManager> _logger;
    private readonly IWritableOptions<AuthOption> _authOption;
    private readonly IWritableOptions<GameConfig> _gameConfig;
    private static bool initialized;

    [AutoPostConstruct]
    public void AutoPostConstruct()
    {
        _apiAuth = new Uri(string.IsNullOrEmpty(_authOption.Value.AuthUrl) ? "https://prd1-auth.mememori-boi.com/api/" : _authOption.Value.AuthUrl);

        _meMoriHttpClientHandler = new MeMoriHttpClientHandler {AppVersion = _authOption.Value.AppVersion};
        _httpClient = new HttpClient(MoriHttpClientHandler);
        if (!Debugger.IsAttached) _httpClient.Timeout = TimeSpan.FromSeconds(10);
        _unityHttpClient = new HttpClient();
        if (!Debugger.IsAttached) _unityHttpClient.Timeout = TimeSpan.FromSeconds(30);
        _unityHttpClient.DefaultRequestHeaders.Add("User-Agent", "UnityPlayer/2021.3.10f1 (UnityWebRequest/1.0, libcurl/7.80.0-DEV)");
        _unityHttpClient.DefaultRequestHeaders.Add("X-Unity-Version", "2021.3.10f1");

        _ = AutoUpdateMasterData();
    }

    public async Task Initialize(Action<string> log = null)
    {
        var response = await GetResponse<GetDataUriRequest, GetDataUriResponse>(new GetDataUriRequest() {CountryCode = "CN"}, log);
        AssetCatalogUriFormat = response.AssetCatalogUriFormat;
        AssetCatalogFixedUriFormat = response.AssetCatalogFixedUriFormat;
        MasterUriFormat = response.MasterUriFormat;
        NoticeBannerImageUriFormat = response.NoticeBannerImageUriFormat;
        AppAssetVersionInfo = response.AppAssetVersionInfo;
        _authOption.Update(x => x.AppVersion = AppAssetVersionInfo.Version);
        initialized = true;
        MoriHttpClientHandler.AppVersion = AppAssetVersionInfo.Version;
    }

    private async Task AutoUpdateMasterData()
    {
        while (!cts.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromHours(1), cts.Token);
                _logger.LogInformation("auto updating master data");
                if (await DownloadMasterCatalog())
                {
                    Masters.LoadAllMasters();
                }
            }
            catch (Exception e) when (e is not TaskCanceledException)
            {
                _logger.LogError(e, "error auto update master data");
            }
        }
    }

    public async Task<bool> DownloadMasterCatalog(Action<string> log = null)
    {
        log ??= Console.WriteLine;
        log(ResourceStrings.Downloading_master_directory___);
        var dataUriResponse = await GetResponse<GetDataUriRequest, GetDataUriResponse>(new GetDataUriRequest() {CountryCode = "CN", UserId = 0});

        var url = string.Format(dataUriResponse.MasterUriFormat, MoriHttpClientHandler.OrtegaMasterVersion, "master-catalog");
        var bytes = await _unityHttpClient.GetByteArrayAsync(url);
        log("Retrieving master catalog...");
        var masterBookCatalog = MessagePackSerializer.Deserialize<MasterBookCatalog>(bytes);
        Directory.CreateDirectory("./Master");
        var hasUpdate = false;
        HashSet<string> allowedLangMb = ["TextResourceJaJpMB", "TextResourceZhTwMB", "TextResourceEnUsMB", "TextResourceKoKrMB"];
        foreach (var (name, info) in masterBookCatalog.MasterBookInfoMap)
        {
            if (name.StartsWith("TextResource") && !allowedLangMb.Contains(name))
            {
                continue;
            }

            var localPath = $"./Master/{name}";
            if (File.Exists(localPath))
            {
                var md5 = await CalcFileMd5(localPath);
                if (md5 == info.Hash)
                {
                    log($"{name} not changed, skip...");
                    continue;
                }

                File.Delete(localPath);
            }

            hasUpdate = true;

            log($"Updating {name}...");
            var mbUrl = string.Format(dataUriResponse.MasterUriFormat, MoriHttpClientHandler.OrtegaMasterVersion, name);
            var fileBytes = await _unityHttpClient.GetByteArrayAsync(mbUrl);
            await File.WriteAllBytesAsync(localPath, fileBytes);
            log($"Finished updating {name}...");
        }

        log(ResourceStrings.Download_master_directory_completed);
        return hasUpdate;
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
            _ => LanguageType.enUS
        };
    }

    public async Task DownloadAssets(string gameOs, string assetsPath, string assetsTmpPath, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Downloading asset catalog...");
        var name = $"{gameOs}/{MoriHttpClientHandler.OrtegaAssetVersion}.json";
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
        await SetServerHost(playerDataInfo.WorldId, log);

        // do login
        var loginPlayerResp = await GetResponse<LoginPlayerRequest, LoginPlayerResponse>(new LoginPlayerRequest
        {
            PlayerId = playerDataInfo.PlayerId, Password = playerDataInfo.Password
        }, log);
        PlayerId = playerDataInfo.PlayerId;
        AuthTokenOfMagicOnion = loginPlayerResp.AuthTokenOfMagicOnion;
    }

    public async Task SetServerHost(long worldId, Action<string> log = null)
    {
        var resp = await GetResponse<GetServerHostRequest, GetServerHostResponse>(new GetServerHostRequest() {WorldId = worldId}, log);
        _apiHost = new Uri(resp.ApiHost);
        _grpcChannel = GrpcChannel.ForAddress(new Uri($"https://{resp.MagicOnionHost}:{resp.MagicOnionPort}"));
    }

    public OrtegaMagicOnionClient GetOnionClient()
    {
        var ortegaMagicOnionClient = new OrtegaMagicOnionClient(_grpcChannel, PlayerId, AuthTokenOfMagicOnion, new MagicOnionLocalRaidNotificaiton());
        return ortegaMagicOnionClient;
    }


    private static readonly SemaphoreSlim semaphoreSlim = new(1, 1);

    public async Task<TResp> GetResponse<TReq, TResp>(TReq req, Action<string> log = null, Action<UserSyncData> userData = null, Uri apiAuth = null, Uri apiHost = null)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase
    {
        await semaphoreSlim.WaitAsync();
        await Task.Delay(_gameConfig.Value.AutoRequestDelay);
        try
        {
            apiHost ??= _apiHost;
            apiAuth ??= _apiAuth;
            log ??= Console.WriteLine;
            var authAttr = typeof(TReq).GetCustomAttribute<OrtegaAuthAttribute>();
            var apiAttr = typeof(TReq).GetCustomAttribute<OrtegaApiAttribute>();
            Uri uri;
            if (authAttr != null)
                uri = new Uri(apiAuth, authAttr.Uri);
            else if (apiAttr != null)
                uri = new Uri(apiHost ?? throw new InvalidOperationException(ResourceStrings.PleaseLogin), apiAttr.Uri);
            else
                throw new NotSupportedException();

            var bytes = MessagePackSerializer.Serialize(req);
            UPDATEREDO:
            try
            {
                using var respMsg = await _httpClient.PostAsync(uri, new ByteArrayContent(bytes) {Headers = {{"content-type", "application/json; charset=UTF-8"}}});
                if (!respMsg.IsSuccessStatusCode) throw new InvalidOperationException(respMsg.ToString());

                await using var stream = await respMsg.Content.ReadAsStreamAsync();
                if (respMsg.Headers.TryGetValues("ortegastatuscode", out var headers2))
                {
                    var ortegastatuscode = headers2.FirstOrDefault() ?? "";
                    if (ortegastatuscode != "0")
                    {
                        var apiErrResponse = MessagePackSerializer.Deserialize<ApiErrorResponse>(stream);

                        if (apiErrResponse.ErrorCode == ErrorCode.CommonRequireClientUpdate)
                        {
                            await GetLatestAvailableVersion();
                            goto UPDATEREDO;
                        }

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

                var response = MessagePackSerializer.Deserialize<TResp>(stream);
                // if (Debugger.IsAttached) log(response.ToJson());
                if (response is IUserSyncApiResponse userSyncApiResponse) userData?.Invoke(userSyncApiResponse.UserSyncData);

                return response;
            }
            catch (TaskCanceledException e)
            {
                throw new Exception(ResourceStrings.Request_timed_out__check_your_network);
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task GetLatestAvailableVersion()
    {
        _logger.LogInformation("auto get latest version...");
        var buildAddCount = 5;
        var minorAddCount = 5;
        var majorAddCount = 5;

        var handler = new MeMoriHttpClientHandler {AppVersion = _authOption.Value.AppVersion};
        var client = new HttpClient(handler);

        while (true)
        {
            var path = typeof(GetDataUriRequest).GetCustomAttribute<OrtegaAuthAttribute>()!.Uri;
            var uri = new Uri(_apiAuth, path);

            var bytes = MessagePackSerializer.Serialize(new GetDataUriRequest() {CountryCode = OrtegaConst.Addressable.LanguageNameDictionary[LanguageType], UserId = UserId});
            using var respMsg = await client.PostAsync(uri, new ByteArrayContent(bytes) {Headers = {{"content-type", "application/json; charset=UTF-8"}}});
            if (!respMsg.IsSuccessStatusCode) throw new InvalidOperationException(respMsg.ToString());

            await using var stream = await respMsg.Content.ReadAsStreamAsync();
            if (respMsg.Headers.TryGetValues("ortegastatuscode", out var headers2))
            {
                var ortegastatuscode = headers2.FirstOrDefault() ?? "";
                if (ortegastatuscode != "0")
                {
                    var apiErrResponse = MessagePackSerializer.Deserialize<ApiErrorResponse>(stream);
                    if (apiErrResponse.ErrorCode != ErrorCode.CommonRequireClientUpdate)
                    {
                        throw new InvalidOperationException(Masters.TextResourceTable.GetErrorCodeMessage(apiErrResponse.ErrorCode));
                    }

                    var version = new Version(handler.AppVersion);
                    if (buildAddCount > 0)
                    {
                        var newVersion = new Version(version.Major, version.Minor, version.Build + 1);
                        handler.AppVersion = newVersion.ToString(3);
                        _logger.LogInformation($"trying {handler.AppVersion}");
                        buildAddCount--;
                        continue;
                    }

                    if (minorAddCount > 0)
                    {
                        var newVersion = new Version(version.Major, version.Minor + 1, 0);
                        handler.AppVersion = newVersion.ToString(3);
                        _logger.LogInformation($"trying {handler.AppVersion}");
                        minorAddCount--;
                        buildAddCount = 5;
                        continue;
                    }

                    if (majorAddCount > 0)
                    {
                        var newVersion = new Version(version.Major + 1, 0, 0);
                        handler.AppVersion = newVersion.ToString(3);
                        _logger.LogInformation($"trying {handler.AppVersion}");
                        majorAddCount--;
                        buildAddCount = 5;
                        minorAddCount = 5;
                        continue;
                    }

                    throw new InvalidOperationException("reached max try out");
                }
                else
                {
                    _logger.LogInformation($"found latest version {handler.AppVersion}");
                    _authOption.Update(x => { x.AppVersion = handler.AppVersion; });
                    MoriHttpClientHandler.AppVersion = handler.AppVersion;
                    return;
                }
            }

            throw new InvalidOperationException("no ortegastatuscode");
        }
    }

    public void Dispose()
    {
        cts.Cancel();
        cts.Dispose();
        _meMoriHttpClientHandler?.Dispose();
        _httpClient?.Dispose();
        _unityHttpClient?.Dispose();
        _grpcChannel?.Dispose();
    }
}