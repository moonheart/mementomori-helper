using System.Collections.Concurrent;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Infrastructure.Database;
using MementoMori.Api.Models;
using MementoMori.Api.Utils;

namespace MementoMori.Api.Services;

/// <summary>
/// 账户上下文 - 每个账户的独立实例
/// </summary>
public class AccountContext
{
    public AccountDto AccountInfo { get; set; } = null!;
    public NetworkManager NetworkManager { get; set; } = null!;
    public TimeManager TimeManager { get; set; } = new();
}

/// <summary>
/// 账户管理器 - 单例服务
/// 管理所有游戏账号及其独立的业务实例，使用 SQLite (FreeSql) 持久化
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class AccountManager
{
    private readonly ILogger<AccountManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IFreeSql _fsql;
    private readonly JobManagerService _jobManagerService;
    
    // 内存中的活跃账户上下文（包含 NetworkManager 和实时登录状态）
    private readonly ConcurrentDictionary<long, Lazy<Task<AccountContext>>> _activeAccounts = new();

    /// <summary>
    /// 获取或创建账户上下文
    /// </summary>
    public async Task<AccountContext> GetOrCreateAsync(long userId)
    {
        return await _activeAccounts.GetOrAdd(userId, id =>
        {
            return new Lazy<Task<AccountContext>>(async () =>
            {
                _logger.LogInformation("Creating account context for user {UserId}", id);

                // 从数据库加载账户信息
                var entity = await _fsql.Select<AccountEntity>()
                    .Where(a => a.UserId == id)
                    .FirstAsync();

                if (entity == null)
                {
                    throw new InvalidOperationException($"Account {id} not found in database");
                }

                var accountInfo = MapToDto(entity);

                // 为该账户创建独立的 NetworkManager 实例
                var networkManager = _serviceProvider.GetRequiredService<NetworkManager>();
                networkManager.UserId = id;

                // 恢复持久化的 AccessToken（若存在）
                if (!string.IsNullOrWhiteSpace(entity.OrtegaAccessToken))
                {
                    networkManager.MoriHttpClientHandler.SetAccessToken(entity.OrtegaAccessToken);
                    _logger.LogInformation("Restored persisted access token for user {UserId}", id);
                }

                // 恢复持久化的设备 UUID（若存在）
                if (!string.IsNullOrWhiteSpace(entity.OrtegaUuid))
                {
                    networkManager.MoriHttpClientHandler.SetOrtegaUuid(entity.OrtegaUuid, false);
                    _logger.LogInformation("Restored persisted ortega uuid for user {UserId}", id);
                }

                // 恢复已持久化的 Game API Host（不发请求，避免 CommonNoSession）
                if (!string.IsNullOrWhiteSpace(entity.GameApiHost))
                {
                    try
                    {
                        networkManager.SetGameApiHost(entity.GameApiHost);
                        _logger.LogInformation("Restored persisted game host for user {UserId}: {GameApiHost}", id, entity.GameApiHost);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to restore persisted game host for user {UserId}", id);
                    }
                }

                // 监听 token 轮换并持久化
                networkManager.MoriHttpClientHandler.AccessTokenUpdated += token =>
                {
                    try
                    {
                        PersistAccessToken(id, token);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to persist access token for user {UserId}", id);
                    }
                };

                // 监听设备 UUID 更新并持久化
                networkManager.MoriHttpClientHandler.OrtegaUuidUpdated += uuid =>
                {
                    try
                    {
                        PersistOrtegaUuid(id, uuid);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to persist ortega uuid for user {UserId}", id);
                    }
                };

                // 确保当前使用的 UUID 已落库（首次接入旧数据时补写）
                PersistOrtegaUuid(id, networkManager.MoriHttpClientHandler.OrtegaUuid);

                // 监听 GameApiHost 更新并持久化
                networkManager.GameApiHostUpdated += gameApiHost =>
                {
                    try
                    {
                        PersistGameApiHost(id, gameApiHost);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to persist game host for user {UserId}", id);
                    }
                };

                // 创建账户上下文
                var context = new AccountContext
                {
                    AccountInfo = accountInfo,
                    NetworkManager = networkManager
                };

                // 检查是否成功还原登录状态（三个条件都必须满足）
                var hasAccessToken = !string.IsNullOrWhiteSpace(entity.OrtegaAccessToken);
                var hasOrtegaUuid = !string.IsNullOrWhiteSpace(entity.OrtegaUuid);
                var hasGameApiHost = !string.IsNullOrWhiteSpace(entity.GameApiHost);

                if (hasAccessToken && hasOrtegaUuid && hasGameApiHost)
                {
                    _logger.LogInformation("Login state restored for user {UserId}, re-registering jobs", id);
                    accountInfo.IsLoggedIn = true;

                    // 重新初始化定时任务
                    try
                    {
                        await _jobManagerService.RegisterJobsAsync(id);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to register jobs for user {UserId} after login state restore", id);
                    }
                }

                return context;
            });
        }).Value;
    }

    /// <summary>
    /// 获取所有账户信息
    /// </summary>
    public List<AccountDto> GetAllAccountInfos()
    {
        var entities = _fsql.Select<AccountEntity>().ToList();
        var dtos = entities.Select(MapToDto).ToList();

        // 填充内存中的登录状态
        foreach (var dto in dtos)
        {
            if (_activeAccounts.TryGetValue(dto.UserId, out var lazyContext) && lazyContext.IsValueCreated)
            {
                // 注意：这里可能需要从已创建的 context 中获取实际状态，
                // 但为了简单，我们假设只要在 activeAccounts 中且已登录就是 true
                // 实际上 UpdateLoginStatus 会同步更新这些 DTO
                dto.IsLoggedIn = lazyContext.Value.Result.AccountInfo.IsLoggedIn;
            }
        }

        return dtos;
    }

    /// <summary>
    /// 添加账户信息
    /// </summary>
    public AccountDto AddAccount(long userId, string clientKey, string name)
    {
        if (_fsql.Select<AccountEntity>().Where(a => a.UserId == userId).Any())
        {
            throw new InvalidOperationException($"Account {userId} already exists");
        }

        var entity = new AccountEntity
        {
            UserId = userId,
            ClientKey = clientKey,
            Name = name
        };

        _fsql.Insert(entity).ExecuteAffrows();

        return MapToDto(entity);
    }

    /// <summary>
    /// 删除账号
    /// </summary>
    public async Task DeleteAccountAsync(long userId)
    {
        // 从数据库删除
        await _fsql.Delete<AccountEntity>().Where(a => a.UserId == userId).ExecuteAffrowsAsync();
        
        // 从活跃缓存中移除并释放资源
        if (_activeAccounts.TryRemove(userId, out var lazyContext))
        {
            if (lazyContext.IsValueCreated)
            {
                var context = await lazyContext.Value;
                context.NetworkManager?.Dispose();
            }
            _logger.LogInformation("Removed active account context for user {UserId}", userId);
        }
    }

    /// <summary>
    /// 更新账号登录状态（仅内存更新持久化部分字段）
    /// </summary>
    public void UpdateLoginStatus(long userId, bool isLoggedIn, long? worldId = null)
    {
        // 1. 更新数据库中的持久化字段（LastLoginTime, CurrentWorldId）
        if (isLoggedIn)
        {
            _fsql.Update<AccountEntity>()
                .Set(a => a.LastLoginTime, DateTime.Now)
                .Set(a => a.CurrentWorldId, worldId)
                .Where(a => a.UserId == userId)
                .ExecuteAffrows();
        }

        // 2. 更新内存中活跃上下文的状态
        if (_activeAccounts.TryGetValue(userId, out var lazyContext) && lazyContext.IsValueCreated)
        {
            var accountInfo = lazyContext.Value.Result.AccountInfo;
            accountInfo.IsLoggedIn = isLoggedIn;
            if (isLoggedIn)
            {
                accountInfo.LastLoginTime = DateTime.Now;
                accountInfo.CurrentWorldId = worldId;
            }
        }
    }

    private void PersistAccessToken(long userId, string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return;
        }

        _fsql.Update<AccountEntity>()
            .Set(a => a.OrtegaAccessToken, token)
            .Where(a => a.UserId == userId)
            .ExecuteAffrows();
    }

    private void PersistGameApiHost(long userId, string gameApiHost)
    {
        if (string.IsNullOrWhiteSpace(gameApiHost))
        {
            return;
        }

        _fsql.Update<AccountEntity>()
            .Set(a => a.GameApiHost, gameApiHost)
            .Where(a => a.UserId == userId)
            .ExecuteAffrows();
    }

    private void PersistOrtegaUuid(long userId, string uuid)
    {
        if (string.IsNullOrWhiteSpace(uuid))
        {
            return;
        }

        _fsql.Update<AccountEntity>()
            .Set(a => a.OrtegaUuid, uuid)
            .Where(a => a.UserId == userId)
            .ExecuteAffrows();
    }

    private AccountDto MapToDto(AccountEntity entity)
    {
        return new AccountDto
        {
            UserId = entity.UserId,
            ClientKey = entity.ClientKey,
            Name = entity.Name,
            LastLoginTime = entity.LastLoginTime,
            CurrentWorldId = entity.CurrentWorldId,
            IsLoggedIn = false // 初始状态为未登录，由 UpdateLoginStatus 或内存状态决定
        };
    }
}
