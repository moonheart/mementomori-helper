using System.Collections.Concurrent;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;

namespace MementoMori.Api.Services;

/// <summary>
/// 账户上下文 - 每个账户的独立实例
/// </summary>
public class AccountContext
{
    public AccountDto AccountInfo { get; set; } = null!;
    public NetworkManager NetworkManager { get; set; } = null!;
    // TODO: 添加其他业务服务实例
    // public ShopService ShopService { get; set; }
    // public MissionService MissionService { get; set; }
}

/// <summary>
/// 账户管理器 - 单例服务
/// 管理所有游戏账号及其独立的业务实例
/// </summary>
public class AccountManager
{
    private readonly ILogger<AccountManager> _logger;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IConfiguration _configuration;
    private readonly ConcurrentDictionary<long, AccountContext> _accounts = new();
    private readonly string _accountsFile;

    public AccountManager(ILogger<AccountManager> logger, ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        _logger = logger;
        _loggerFactory = loggerFactory;
        _configuration = configuration;
        _accountsFile = Path.Combine(Directory.GetCurrentDirectory(), "accounts.json");
        
        // 启动时加载账户信息（不创建实例）
        LoadAccountInfos();
    }

    /// <summary>
    /// 获取或创建账户上下文
    /// </summary>
    public AccountContext GetOrCreate(long userId)
    {
        return _accounts.GetOrAdd(userId, id =>
        {
            _logger.LogInformation("Creating account context for user {UserId}", id);
            
            // 加载账户信息
            var accountInfo = LoadAccountInfos().FirstOrDefault(a => a.UserId == id);
            if (accountInfo == null)
            {
                throw new InvalidOperationException($"Account {id} not found");
            }

            // 为该账户创建独立的 NetworkManager 实例
            var networkManagerLogger = _loggerFactory.CreateLogger<NetworkManager>();
            var networkManager = new NetworkManager(networkManagerLogger, _configuration);

            // 设置 UserId
            networkManager.UserId = id;

            // 创建账户上下文
            var context = new AccountContext
            {
                AccountInfo = accountInfo,
                NetworkManager = networkManager
            };

            return context;
        });
    }

    /// <summary>
    /// 获取所有账户信息（不创建实例）
    /// </summary>
    public List<AccountDto> GetAllAccountInfos()
    {
        return LoadAccountInfos();
    }

    /// <summary>
    /// 添加账户信息
    /// </summary>
    public AccountDto AddAccount(long userId, string clientKey, string name)
    {
        var accounts = LoadAccountInfos();
        
        if (accounts.Any(a => a.UserId == userId))
        {
            throw new InvalidOperationException($"Account {userId} already exists");
        }

        var account = new AccountDto
        {
            UserId = userId,
            ClientKey = clientKey,
            Name = name
        };

        accounts.Add(account);
        SaveAccountInfos(accounts);

        return account;
    }

    /// <summary>
    /// 删除账号
    /// </summary>
    public void DeleteAccount(long userId)
    {
        var accounts = LoadAccountInfos();
        var account = accounts.FirstOrDefault(a => a.UserId == userId);
        
        if (account != null)
        {
            accounts.Remove(account);
            SaveAccountInfos(accounts);
            
            // 从缓存中移除
            if (_accounts.TryRemove(userId, out var context))
            {
                // 释放资源
                context.NetworkManager?.Dispose();
                _logger.LogInformation("Removed account context for user {UserId}", userId);
            }
        }
    }

    /// <summary>
    /// 更新账号登录状态
    /// </summary>
    public void UpdateLoginStatus(long userId, bool isLoggedIn, long? worldId = null)
    {
        var accounts = LoadAccountInfos();
        var account = accounts.FirstOrDefault(a => a.UserId == userId);
        
        if (account != null)
        {
            account.IsLoggedIn = isLoggedIn;
            account.LastLoginTime = isLoggedIn ? DateTime.Now : account.LastLoginTime;
            account.CurrentWorldId = worldId;
            SaveAccountInfos(accounts);
        }
    }

    /// <summary>
    /// 加载账户信息
    /// </summary>
    private List<AccountDto> LoadAccountInfos()
    {
        if (!File.Exists(_accountsFile))
        {
            return new List<AccountDto>();
        }

        try
        {
            var json = File.ReadAllText(_accountsFile);
            return System.Text.Json.JsonSerializer.Deserialize<List<AccountDto>>(json) 
                   ?? new List<AccountDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load accounts");
            return new List<AccountDto>();
        }
    }

    /// <summary>
    /// 保存账户信息
    /// </summary>
    private void SaveAccountInfos(List<AccountDto> accounts)
    {
        try
        {
            var json = System.Text.Json.JsonSerializer.Serialize(accounts, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_accountsFile, json);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save accounts");
            throw;
        }
    }
}
