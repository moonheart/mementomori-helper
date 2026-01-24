using System.Collections.Concurrent;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Infrastructure.Database;
using MementoMori.Api.Models;

namespace MementoMori.Api.Services;

/// <summary>
/// 账户上下文 - 每个账户的独立实例
/// </summary>
public class AccountContext
{
    public AccountDto AccountInfo { get; set; } = null!;
    public NetworkManager NetworkManager { get; set; } = null!;
}

/// <summary>
/// 账户管理器 - 单例服务
/// 管理所有游戏账号及其独立的业务实例，使用 SQLite (FreeSql) 持久化
/// </summary>
public class AccountManager
{
    private readonly ILogger<AccountManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IFreeSql _fsql;
    
    // 内存中的活跃账户上下文（包含 NetworkManager 和实时登录状态）
    private readonly ConcurrentDictionary<long, Lazy<Task<AccountContext>>> _activeAccounts = new();

    public AccountManager(ILogger<AccountManager> logger, IServiceProvider serviceProvider, IFreeSql fsql)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _fsql = fsql;
    }

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

                // 创建账户上下文
                var context = new AccountContext
                {
                    AccountInfo = accountInfo,
                    NetworkManager = networkManager
                };
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
