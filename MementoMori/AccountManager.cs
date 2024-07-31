using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.CompilerServices;
using AutoCtor;
using Injectio.Attributes;
using MementoMori.Funcs;
using MementoMori.Option;
using MementoMori.Ortega.Share.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace MementoMori;

[RegisterSingleton<AccountManager>]
[AutoConstruct]
public partial class AccountManager : ReactiveObject
{
    private readonly ConcurrentDictionary<long, Account> _accounts = new();
    private readonly IWritableOptions<AuthOption> _authOption;
    private readonly IWritableOptions<GameConfig> _gameConfig;
    private readonly ILogger<AccountManager> _logger;
    private readonly IServiceProvider _serviceProvider;
    private CultureInfo _currentCulture;
    private long _currentUserId;

    public Account Current => Get(CurrentUserId);

    public CultureInfo CurrentCulture
    {
        get => _currentCulture;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentCulture, value);
            foreach (var account in _accounts.Values)
            {
                account.NetworkManager.SetCultureInfo(value);
            }

            CultureInfo.DefaultThreadCurrentCulture = value;
            CultureInfo.DefaultThreadCurrentUICulture = value;
        }
    }

    public long CurrentUserId
    {
        get
        {
            if (_currentUserId <= 0) _currentUserId = _authOption.Value.Accounts.FirstOrDefault()?.UserId ?? 0;
            return _currentUserId;
        }
        set => this.RaiseAndSetIfChanged(ref _currentUserId, value);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
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
            account.NetworkManager.UserId = userId;
            _accounts[userId] = account;
        }

        return _accounts[userId];
    }

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

    public Dictionary<long, Account> GetAll()
    {
        return _accounts.ToDictionary(x => x.Key, x => x.Value);
    }

    public void MigrateToAccountArray()
    {
        if (_authOption.Value.Accounts.Count == 0 && _authOption.Value.UserId > 0 && !_authOption.Value.ClientKey.IsNullOrEmpty())
        {
            _authOption.Update(opt =>
            {
                opt.Accounts = new List<AccountInfo>
                {
                    new()
                    {
                        UserId = opt.UserId,
                        ClientKey = opt.ClientKey,
                        Name = ResourceStrings.MainAccount,
                        AutoLogin = _gameConfig.Value.Login.AutoLogin
                    }
                };
            });
        }
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

    public void UpdateAccountInfo(long userId)
    {
        Get(userId).AccountInfo = GetAccountInfo(userId);
    }

    public void RemoveAccount(long userId)
    {
        _accounts.TryRemove(userId, out _);
        _authOption.Update(opt => { opt.Accounts.RemoveAll(x => x.UserId == userId); });
    }
}

public class Account
{
    public AccountInfo AccountInfo { get; set; }
    public MementoNetworkManager NetworkManager { get; set; }
    public MementoMoriFuncs Funcs { get; set; }
}