using MementoMori.Api.Models;
using MementoMori.Ortega.Share.Data.ApiInterface.User;

namespace MementoMori.Api.Services;

/// <summary>
/// 账户服务 - 简化版，使用 AccountManager
/// </summary>
[RegisterScoped]
[AutoConstructor]
public partial class AccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly AccountManager _accountManager;
    private readonly AccountCredentialService _credentialService;
    private readonly JobManagerService _jobManager;
    private readonly PlayerSettingService _settingService;
    private readonly Autopilot.GrowthTracker _growthTracker;

    public List<AccountDto> GetAllAccounts()
    {
        return _accountManager.GetAllAccountInfos();
    }

    public async Task<AccountDto> AddAccountAsync(long userId, string clientKey, string name)
    {
        var account = _accountManager.AddAccount(userId, clientKey, name);
        await _settingService.InitializeDefaultSettingsAsync(userId);
        return account;
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
            await _settingService.InitializeDefaultSettingsAsync(userId);

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

    public record WorldInfo(long WorldId, long PlayerId, string Name, long PlayerRank, long LastLoginTime, bool Current);

    /// <summary>这个账号在每个世界的角色 (同一个引继码可以在多个世界各有一个角色)</summary>
    public async Task<List<WorldInfo>> GetWorldsAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var list = await context.NetworkManager.GetPlayerDataInfoListAsync(new MementoMori.Ortega.Share.Data.ApiInterface.Auth.LoginRequest
        {
            UserId = userId, ClientKey = context.AccountInfo.ClientKey
        });
        return (list ?? new()).OrderByDescending(p => p.PlayerRank)
            .Select(p => new WorldInfo(p.WorldId, p.PlayerId, p.Name, p.PlayerRank, p.LastLoginTime, p.WorldId == context.AccountInfo.CurrentWorldId))
            .ToList();
    }

    /// <summary>换到另一个世界的角色并重新登录 (之后自动登录也会留在这个世界)</summary>
    public async Task<SimpleLoginResponse> SwitchWorldAsync(long userId, long worldId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        _accountManager.UpdateLoginStatus(userId, false);
        context.AccountInfo.CurrentWorldId = worldId; // LoginAsync 会优先选它, 登录成功后写进数据库
        return await LoginAsync(userId, context.AccountInfo.ClientKey);
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
            // 留在上次用的世界; 没有就选等级最高的角色. 以前选 "最近登录" 的世界,
            // 玩家在别的世界开个新号 (或用游戏客户端登录过) 就会被切到那个 2 级的新号上
            var latestWorld = playerDataList.FirstOrDefault(p => p.WorldId == context.AccountInfo.CurrentWorldId)
                              ?? playerDataList.OrderByDescending(p => p.PlayerRank).ThenByDescending(p => p.LastLoginTime).First();
            
            // 4. 执行真正登录流程（发送 LoginPlayerRequest，建立 Session）
            _logger.LogInformation("Attempting full login for user {UserId} on World {WorldId}", userId, latestWorld.WorldId);
            await nm.LoginAsync(latestWorld.WorldId);

            // 4.1 同步 TimeServer 到账户级 TimeManager
            if (nm.TimeServerMb != null)
            {
                context.TimeManager.SetTimeServerMb(nm.TimeServerMb);
            }

            // 4.2 获取用户数据（参考原始 Blazor 版本的 AuthLogin 流程）
            _logger.LogInformation("Fetching user data for user {UserId}", userId);
            await nm.SendRequest<GetUserDataRequest, GetUserDataResponse>(new GetUserDataRequest());

            // 5. 更新本地账号登录状态
            _accountManager.UpdateLoginStatus(userId, true, latestWorld.WorldId);

            // 6. 注册/刷新定时任务
            await _jobManager.RegisterJobsAsync(userId);
            await _growthTracker.SnapshotAsync(userId, context.NetworkManager.UserSyncData);
            
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
