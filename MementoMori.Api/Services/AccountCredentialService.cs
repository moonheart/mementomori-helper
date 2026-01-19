using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Enums;
using Newtonsoft.Json.Linq;

namespace MementoMori.Api.Services;

/// <summary>
/// 账号凭证服务 - 通过密码获取 ClientKey
/// </summary>
public class AccountCredentialService
{
    private readonly ILogger<AccountCredentialService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly NetworkManager _networkManager;

    public AccountCredentialService(
        ILogger<AccountCredentialService> logger,
        IHttpClientFactory httpClientFactory,
        NetworkManager networkManager)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _networkManager = networkManager;
    }

    /// <summary>
    /// 通过密码（引继码）获取 ClientKey
    /// 迁移自 MementoMoriFuncs.GetClientKey
    /// </summary>
    public async Task<string> GetClientKeyAsync(long userId, string password)
    {
        try
        {
            // 1. 获取 AuthToken
            var authToken = await GetAuthTokenAsync();
            if (authToken == 0)
            {
                throw new InvalidOperationException("Failed to get AuthToken");
            }

            // 2. 创建临时用户
            var createUserResponse = await _networkManager.SendRequest<CreateUserRequest, CreateUserResponse>(
                new CreateUserRequest
                {
                    AdverisementId = Guid.NewGuid().ToString("D"),
                    AppVersion = _networkManager.AppVersion,
                    CountryCode = "CN",
                    DeviceToken = "",
                    ModelName = "API",
                    DisplayLanguage = LanguageType.zhTW,
                    OSVersion = "Windows 10",
                    SteamTicket = "",
                    AuthToken = authToken
                }, 
                useAuthApi: true);

            _logger.LogInformation("Created temporary user: {UserId}", createUserResponse.UserId);

            // 3. 获取引继用户数据
            var getComebackUserDataResponse = await _networkManager.SendRequest<GetComebackUserDataRequest, GetComebackUserDataResponse>(
                new GetComebackUserDataRequest
                {
                    FromUserId = createUserResponse.UserId,
                    Password = password,
                    SnsType = SnsType.OrtegaId,
                    UserId = userId,
                    AuthToken = authToken
                },
                useAuthApi: true);

            _logger.LogInformation("Got comeback data, OneTimeToken obtained");

            // 4. 执行引继
            var comebackUserResponse = await _networkManager.SendRequest<ComebackUserRequest, ComebackUserResponse>(
                new ComebackUserRequest
                {
                    FromUserId = createUserResponse.UserId,
                    OneTimeToken = getComebackUserDataResponse.OneTimeToken,
                    ToUserId = userId
                },
                useAuthApi: true);

            _logger.LogInformation("Comeback successful, got ClientKey for user {UserId}", userId);

            return comebackUserResponse.ClientKey;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get ClientKey for user {UserId}", userId);
            throw;
        }
    }

    /// <summary>
    /// 获取 AuthToken
    /// </summary>
    private async Task<int> GetAuthTokenAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"https://list.moonheart.dev/d/public/mmtm/AddressableLocalAssets/ScriptableObjects/AuthToken/AuthTokenData.json?v={DateTimeOffset.Now.ToUnixTimeSeconds()}";
            var json = await client.GetStringAsync(url);
            var token = JObject.Parse(json)["_authToken"]?.Value<int>() ?? 0;
            
            _logger.LogInformation("Got AuthToken: {Token}", token);
            return token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get AuthToken");
            return 0;
        }
    }
}
