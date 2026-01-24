using MementoMori.Api.Models;

namespace MementoMori.Api.Services;

/// <summary>
/// 账户服务 - 简化版，使用 AccountManager
/// </summary>
public class AccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly AccountManager _accountManager;
    private readonly AccountCredentialService _credentialService;

    public AccountService(
        ILogger<AccountService> logger,
        AccountManager accountManager,
        AccountCredentialService credentialService)
    {
        _logger = logger;
        _accountManager = accountManager;
        _credentialService = credentialService;
    }

    public List<AccountDto> GetAllAccounts()
    {
        return _accountManager.GetAllAccountInfos();
    }

    public AccountDto AddAccount(long userId, string clientKey, string name)
    {
        return _accountManager.AddAccount(userId, clientKey, name);
    }

    /// <summary>
    /// 添加账号 - 使用密码（引继码）
    /// </summary>
    public async Task<GetClientKeyResponse> AddAccountWithPasswordAsync(long userId, string password, string name)
    {
        try
        {
            // 获取 ClientKey
            var clientKey = await _credentialService.GetClientKeyAsync(userId, password);
            
            if (string.IsNullOrEmpty(clientKey))
            {
                return new GetClientKeyResponse
                {
                    Success = false,
                    Message = "Failed to get ClientKey from password"
                };
            }

            // 添加账号
            _accountManager.AddAccount(userId, clientKey, name);

            return new GetClientKeyResponse
            {
                Success = true,
                ClientKey = clientKey,
                Message = "Account added successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add account with password");
            return new GetClientKeyResponse
            {
                Success = false,
                Message = ex.Message
            };
        }
    }

    public async Task DeleteAccountAsync(long userId)
    {
        await _accountManager.DeleteAccountAsync(userId);
    }

    public async Task<SimpleLoginResponse> LoginAsync(long userId, string clientKey)
    {
        try
        {
            // 1. 获取账户上下文（包含专属 NetworkManager）
            var context = await _accountManager.GetOrCreateAsync(userId);
            var nm = context.NetworkManager;

            // 2. 获取玩家数据列表（这会同时在 NetworkManager 内部缓存 LoginRequest）
            var loginRequest = new MementoMori.Ortega.Share.Data.ApiInterface.Auth.LoginRequest
            {
                UserId = userId,
                ClientKey = clientKey
            };
            var playerDataList = await nm.GetPlayerDataInfoListAsync(loginRequest);
            
            if (playerDataList == null || playerDataList.Count == 0)
            {
                return new SimpleLoginResponse
                {
                    Success = false,
                    UserId = userId,
                    PlayerName = context.AccountInfo.Name,
                    Message = "No available worlds found"
                };
            }

            // 3. 选择最近登录的世界
            var latestWorld = playerDataList.OrderByDescending(p => p.LastLoginTime).First();
            
            // 4. 执行真正登录流程（发送 LoginPlayerRequest，建立 Session）
            _logger.LogInformation("Attempting full login for user {UserId} on World {WorldId}", userId, latestWorld.WorldId);
            await nm.LoginAsync(latestWorld.WorldId);
            
            // 5. 更新本地账号登录状态
            _accountManager.UpdateLoginStatus(userId, true, latestWorld.WorldId);
            
            _logger.LogInformation("Login successful and Session established. WorldId: {WorldId}", latestWorld.WorldId);
            
            return new SimpleLoginResponse
            {
                Success = true,
                UserId = userId,
                PlayerName = context.AccountInfo.Name,
                Message = $"Logged in to World {latestWorld.WorldId}",
                WorldId = latestWorld.WorldId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for user {UserId}", userId);
            return new SimpleLoginResponse
            {
                Success = false,
                UserId = userId,
                PlayerName = "",
                Message = ex.Message
            };
        }
    }
}
