using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Option;
using MementoMori.Ortega.Share.Extensions;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace MementoMori;

public class AccountManager : ReactiveObject
{
    private readonly IWritableOptions<AuthOption> _authOption;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AccountManager> _logger;

    private readonly ConcurrentDictionary<long, Account> _accounts = new();
    private CultureInfo _currentCulture;
    private long _currentUserId;

    public AccountManager(IWritableOptions<AuthOption> authOption, IWritableOptions<GameConfig> gameConfig, IServiceProvider serviceProvider, ILogger<AccountManager> logger)
    {
        _authOption = authOption;
        _gameConfig = gameConfig;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Account Get(long userId)
    {
        if (!_accounts.ContainsKey(userId))
        {
            var account = new Account
            {
                AccountInfo = _authOption.Value.Accounts.FirstOrDefault(x => x.UserId == userId),
                NetworkManager = _serviceProvider.GetService<MementoNetworkManager>(),
                Funcs = _serviceProvider.GetService<MementoMoriFuncs>()
            };
            account.Funcs.NetworkManager = account.NetworkManager;
            account.Funcs.UserId = userId;
            _accounts[userId] = account;
        }

        return _accounts[userId];
    }

    public Account Current => Get(CurrentUserId);

    public bool Contains(long userId)
    {
        return _accounts.ContainsKey(userId);
    }

    public void AddAccountInfo(long userId, string clientKey, string name, bool autoLogin)
    {
        _authOption.Update(opt =>
        {
            opt.Accounts.Add(new AccountInfo
            {
                UserId = userId,
                ClientKey = clientKey,
                Name = name,
                AutoLogin = autoLogin
            });
        });
    }

    public AccountInfo GetAccountInfo(long userId)
    {
        return _authOption.Value.Accounts.FirstOrDefault(x => x.UserId == userId);
    }

    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentCulture, value);
            foreach (var account in _accounts.Values) account.NetworkManager.SetCultureInfo(value);
        }
    }

    public Dictionary<long, Account> GetAll()
    {
        return _accounts.ToDictionary(x => x.Key, x => x.Value);
    }

    public void MigrateToAccountArray()
    {
        if (_authOption.Value.Accounts.Count == 0 && _authOption.Value.UserId > 0 && !_authOption.Value.ClientKey.IsNullOrEmpty())
            _authOption.Update(opt =>
            {
                opt.Accounts = new List<AccountInfo>()
                {
                    new()
                    {
                        UserId = opt.UserId,
                        ClientKey = opt.ClientKey,
                        Name = "主账号",
                        AutoLogin = _gameConfig.Value.Login.AutoLogin
                    }
                };
            });
    }

    public async Task AutoLogin()
    {
        foreach (var account in _authOption.Value.Accounts)
        {
            if (account.AutoLogin)
            {
                try
                {
                    await Get(account.UserId).Funcs.AutoLogin();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "AutoLogin error");
                }
            }
        }
    }

    public long CurrentUserId
    {
        get
        {
            if (_currentUserId <= 0) _currentUserId = _authOption.Value.Accounts.First().UserId;
            return _currentUserId;
        }
        set => this.RaiseAndSetIfChanged(ref _currentUserId, value);
    }

    public void UpdateAccountInfo(long userId)
    {
        Get(userId).AccountInfo = GetAccountInfo(userId);
    }
}

public class Account
{
    public AccountInfo AccountInfo { get; set; }
    public MementoNetworkManager NetworkManager { get; set; }
    public MementoMoriFuncs Funcs { get; set; }
}