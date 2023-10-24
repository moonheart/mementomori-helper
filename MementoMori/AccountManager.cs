using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Option;
using MementoMori.Ortega.Share.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace MementoMori;

public class AccountManager : ReactiveObject
{
    private readonly IWritableOptions<AuthOption> _authOption;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    private readonly IServiceProvider _serviceProvider;

    private readonly ConcurrentDictionary<long, Account> _accounts = new();
    private CultureInfo _currentCulture;
    private long _currentUserId;

    public AccountManager(IWritableOptions<AuthOption> authOption, IWritableOptions<GameConfig> gameConfig, IServiceProvider serviceProvider)
    {
        _authOption = authOption;
        _gameConfig = gameConfig;
        _serviceProvider = serviceProvider;
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

    public void AddAccountInfo(long userId, string clientKey)
    {
        _authOption.Update(opt =>
        {
            opt.Accounts.Add(new AccountInfo
            {
                UserId = userId,
                ClientKey = clientKey,
                AutoLogin = _gameConfig.Value.Login.AutoLogin
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
            _currentCulture = value;
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
                        ClientKey = opt.DeviceToken,
                        AutoLogin = _gameConfig.Value.Login.AutoLogin
                    }
                };
            });
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
}

public class Account
{
    public AccountInfo AccountInfo { get; set; }
    public MementoNetworkManager NetworkManager { get; set; }
    public MementoMoriFuncs Funcs { get; set; }
}