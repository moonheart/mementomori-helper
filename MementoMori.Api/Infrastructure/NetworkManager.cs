using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.Auth;
using MementoMori.Ortega.Share.Enums;
using MessagePack;
using Grpc.Net.Client;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Master;

namespace MementoMori.Api.Infrastructure;

/// <summary>
/// 网络管理器 - 完整版
/// 迁移自 MementoMori.MementoNetworkManager
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class NetworkManager : IDisposable
{
    private readonly ILogger<NetworkManager> _logger;
    private readonly IConfiguration _configuration;
    private readonly VersionService _versionService;

    [AutoConstructorIgnore]
    private readonly SemaphoreSlim _requestSemaphore = new(1, 1);
    [AutoConstructorIgnore]
    private readonly CancellationTokenSource _cts = new();

    // HTTP Clients
    public MeMoriHttpClientHandler MoriHttpClientHandler { get; private set; } = null!;
    private HttpClient _httpClient = null!;
    private HttpClient _unityHttpClient = null!; // 用于下载 Master 数据

    // API URLs
    private Uri _authApiUrl = null!;
    private Uri? _gameApiUrl;
    private GrpcChannel? _grpcChannel;

    // State
    public long UserId { get; set; }
    public long PlayerId { get; set; }
    public CultureInfo CultureInfo { get; private set; } = new("zh-CN");
    public LanguageType LanguageType => ParseLanguageType(CultureInfo);

    // MagicOnion
    private string? _authTokenOfMagicOnion;

    // Last login request
    private LoginRequest? _lastLoginRequest;

    // Synchronized data
    public UserSyncData UserSyncData { get; private set; } = new();

    [AutoConstructorInitializer]
    private void InitializeInternal()
    {
        // 从配置读取 Auth API URL，如果没有则使用默认值
        var authUrl = _configuration["Auth:AuthUrl"];
        _authApiUrl = new Uri(string.IsNullOrEmpty(authUrl)
            ? "https://prd1-auth.mememori-boi.com/api/"
            : authUrl);

        // 创建 HTTP 处理器
        MoriHttpClientHandler = new MeMoriHttpClientHandler
        {
            AppVersion = _versionService.AppVersion
        };

        // 创建 HTTP 客户端
        _httpClient = new HttpClient(MoriHttpClientHandler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };

        // 创建 Unity HTTP 客户端（用于某些特殊请求）
        _unityHttpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
        _unityHttpClient.DefaultRequestHeaders.Add("User-Agent", "UnityPlayer/2021.3.10f1 (UnityWebRequest/1.0, libcurl/7.80.0-DEV)");
        _unityHttpClient.DefaultRequestHeaders.Add("X-Unity-Version", "2021.3.10f1");

        _logger.LogInformation("NetworkManager initialized - AuthUrl: {AuthUrl}, AppVersion: {AppVersion}",
            _authApiUrl, _versionService.AppVersion);
    }

    /// <summary>
    /// 初始化 - 获取数据 URI
    /// </summary>
    public async Task InitializeAsync(string countryCode = "CN")
    {
        try
        {
            // 确保版本信息已刷新
            if (string.IsNullOrEmpty(_versionService.MasterUriFormat))
            {
                await _versionService.RefreshVersionAsync();
            }

            MoriHttpClientHandler.AppVersion = _versionService.AppVersion;

            _logger.LogInformation("NetworkManager initialized. AppVersion: {Version}", _versionService.AppVersion);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize NetworkManager");
            throw;
        }
    }

    /// <summary>
    /// 获取玩家数据列表
    /// </summary>
    public async Task<List<PlayerDataInfo>> GetPlayerDataInfoListAsync(LoginRequest loginRequest)
    {
        _lastLoginRequest = loginRequest;
        var response = await SendRequest<LoginRequest, LoginResponse>(loginRequest, useAuthApi: true);
        return response.PlayerDataInfoList?.ToList() ?? new List<PlayerDataInfo>();
    }

    /// <summary>
    /// 登录到指定世界
    /// </summary>
    public async Task LoginAsync(long worldId)
    {
        if (_lastLoginRequest == null)
        {
            throw new InvalidOperationException("Must call GetPlayerDataInfoListAsync first");
        }

        // 重新获取玩家数据
        var response = await SendRequest<LoginRequest, LoginResponse>(_lastLoginRequest, useAuthApi: true);
        var playerDataInfo = response.PlayerDataInfoList?.FirstOrDefault(x => x.WorldId == worldId);

        if (playerDataInfo == null)
        {
            throw new InvalidOperationException($"World {worldId} not found");
        }

        // 设置服务器 Host
        await SetServerHostAsync(worldId);

        // 登录玩家
        var loginPlayerRequest = new LoginPlayerRequest
        {
            PlayerId = playerDataInfo.PlayerId,
            Password = playerDataInfo.Password
        };

        var loginPlayerResponse = await SendRequest<LoginPlayerRequest, LoginPlayerResponse>(
            loginPlayerRequest, useAuthApi: false);

        PlayerId = playerDataInfo.PlayerId;
        _authTokenOfMagicOnion = loginPlayerResponse.AuthTokenOfMagicOnion;

        _logger.LogInformation("Logged in to world {WorldId}, PlayerId: {PlayerId}", worldId, PlayerId);
    }

    /// <summary>
    /// 设置游戏服务器 Host
    /// </summary>
    public async Task SetServerHostAsync(long worldId)
    {
        var request = new GetServerHostRequest
        {
            WorldId = worldId
        };

        var response = await SendRequest<GetServerHostRequest, GetServerHostResponse>(request, useAuthApi: true);

        _gameApiUrl = new Uri(response.ApiHost);
        _grpcChannel?.Dispose();
        _grpcChannel = GrpcChannel.ForAddress(new Uri($"https://{response.MagicOnionHost}:{response.MagicOnionPort}"));

        _logger.LogInformation("Game API URL set to {Url}", _gameApiUrl);
    }


    /// <summary>
    /// 设置语言文化
    /// </summary>
    public void SetCultureInfo(CultureInfo cultureInfo)
    {
        CultureInfo = cultureInfo;
        // TODO: TextResourceTable.SetLanguageType(ParseLanguageType(cultureInfo));
        // TODO: LoadAllMasters();
        _logger.LogInformation("Culture set to {Culture}", cultureInfo.Name);
    }

    /// <summary>
    /// 解析语言类型
    /// </summary>
    private LanguageType ParseLanguageType(CultureInfo cultureInfo)
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

    /// <summary>
    /// 计算文件 MD5
    /// </summary>
    private async Task<string> CalcFileMd5Async(string path)
    {
        using var file = new FileStream(path, FileMode.Open);
        using var md5 = System.Security.Cryptography.MD5.Create();
        var hash = await md5.ComputeHashAsync(file);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    /// <summary>
    /// 发送 API 请求（增强版 - 包含自动重试、错误处理等）
    /// </summary>
    public async Task<TResp> SendRequest<TReq, TResp>(TReq request, bool useAuthApi = true, Action<UserSyncData>? userDataCallback = null)
        where TReq : ApiRequestBase
        where TResp : ApiResponseBase
    {
        await _requestSemaphore.WaitAsync();
        try
        {
            // 自动延迟（如果配置了）
            var autoDelay = _configuration.GetValue<int>("Game:AutoRequestDelay", 0);
            if (autoDelay > 0)
            {
                await Task.Delay(autoDelay);
            }

            var apiUrl = useAuthApi ? _authApiUrl : (_gameApiUrl ?? _authApiUrl);

            // 获取 API 路径
            var path = GetApiPath<TReq>();
            var uri = new Uri(apiUrl, path);

            _logger.LogDebug("Sending request to {Uri}", uri);

            // 序列化请求
            var requestBytes = MessagePackSerializer.Serialize(request);

        // 重试标签（用于自动更新后重试）
        RETRY_AFTER_UPDATE:
            try
            {
                // 发送请求
                using var content = new ByteArrayContent(requestBytes);
                content.Headers.Add("content-type", "application/json; charset=UTF-8");

                using var response = await _httpClient.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("HTTP Error: {StatusCode}", response.StatusCode);
                    throw new InvalidOperationException($"HTTP request failed: {response.StatusCode}");
                }

                // 检查 Ortega 状态码
                if (response.Headers.TryGetValues("ortegastatuscode", out var statusCodes))
                {
                    var statusCode = statusCodes.FirstOrDefault();
                    if (statusCode != "0")
                    {
                        await using var errorStream = await response.Content.ReadAsStreamAsync();
                        var error = MessagePackSerializer.Deserialize<ApiErrorResponse>(errorStream);

                        // 特殊处理：需要客户端更新
                        if (error.ErrorCode == ErrorCode.CommonRequireClientUpdate)
                        {
                            _logger.LogWarning("Client update required, attempting auto-update via VersionService...");
                            await _versionService.RefreshVersionAsync();
                            MoriHttpClientHandler.AppVersion = _versionService.AppVersion;

                            // 更新请求中的字节（如果有必要，虽然 GetDataUriRequest 没传版本，但以防万一）
                            goto RETRY_AFTER_UPDATE; // 更新后重试
                        }

                        // 特殊错误日志
                        if (error.ErrorCode == ErrorCode.InvalidRequestHeader)
                        {
                            _logger.LogError("Login expired, please log in again");
                        }
                        else if (error.ErrorCode == ErrorCode.AuthLoginInvalidRequest)
                        {
                            _logger.LogError("Login failed, please check your account configuration");
                        }
                        else if (error.ErrorCode == ErrorCode.CommonNoSession)
                        {
                            _logger.LogError("No session found");
                        }

                        // 详细错误日志
                        _logger.LogError("API Error at {Uri}: {ErrorCode}", uri, error.ErrorCode);

                        // 抛出自定义异常
                        throw new ApiErrorException(error.ErrorCode);
                    }
                }

                // 反序列化响应
                await using var stream = await response.Content.ReadAsStreamAsync();
                var result = MessagePackSerializer.Deserialize<TResp>(stream);

                // 处理 UserSyncData（增量合并）
                if (result is IUserSyncApiResponse userSyncResponse)
                {
                    if (userSyncResponse.UserSyncData != null)
                    {
                        // 使用 UserItemEditorMergeUserSyncData 进行增量合并
                        UserSyncData.UserItemEditorMergeUserSyncData(userSyncResponse.UserSyncData);
                    }
                    userDataCallback?.Invoke(userSyncResponse.UserSyncData);
                }

                _logger.LogDebug("Request successful");
                return result;
            }
            catch (TaskCanceledException)
            {
                _logger.LogError("Request timed out, check your network");
                throw new Exception("Request timed out, check your network");
            }
        }
        finally
        {
            _requestSemaphore.Release();
        }
    }

    /// <summary>
    /// API 错误异常
    /// </summary>
    public class ApiErrorException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public ApiErrorException(ErrorCode errorCode)
            : base($"API Error: {errorCode}")
        {
            ErrorCode = errorCode;
        }
    }

    /// <summary>
    /// 获取 API 路径
    /// </summary>
    private string GetApiPath<TReq>() where TReq : ApiRequestBase
    {
        var type = typeof(TReq);

        // 尝试从 Attribute 获取路径
        var authAttr = type.GetCustomAttribute<OrtegaAuthAttribute>();
        if (authAttr != null)
            return authAttr.Uri;

        var apiAttr = type.GetCustomAttribute<OrtegaApiAttribute>();
        if (apiAttr != null)
            return apiAttr.Uri;

        // Fallback
        var typeName = type.Name;
        return typeName switch
        {
            "GetDataRequest" => "auth/get-data",
            "LoginRequest" => "auth/login",
            "GetDataUriRequest" => "auth/get-data-uri",
            "LoginPlayerRequest" => "login-player",
            "GetServerHostRequest" => "auth/get-server-host",
            _ => $"api/{typeName.ToLowerInvariant()}"
        };
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
        _requestSemaphore?.Dispose();
        _httpClient?.Dispose();
        _unityHttpClient?.Dispose();
        MoriHttpClientHandler?.Dispose();
        _grpcChannel?.Dispose();
    }
}
