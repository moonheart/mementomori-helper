using System.Reactive.Linq;
using MementoMori.Funcs;
using Microsoft.AspNetCore.Components;
using ReactiveUI;

namespace MementoMori.BlazorShared.Models;

public class AccountComponent : ComponentBase
{
    protected AccountInfo AccountInfo = null!;
    protected MementoMoriFuncs Funcs = null!;
    protected MementoNetworkManager NetworkManager = null!;

    [Inject]
    public AccountManager AccountManager { get; set; }

    protected virtual Task AccountChanged()
    {
        return Task.CompletedTask;
    }

    protected override async Task OnInitializedAsync()
    {
        AccountInfo = AccountManager.Current.AccountInfo;
        Funcs = AccountManager.Current.Funcs;
        NetworkManager = AccountManager.Current.NetworkManager;
        await AccountChanged();
        AccountManager.WhenAnyValue(d => d.CurrentUserId).Throttle(TimeSpan.FromMilliseconds(100)).Subscribe(async userId =>
        {
            var account = AccountManager.Get(userId);
            AccountInfo = account.AccountInfo;
            Funcs = account.Funcs;
            NetworkManager = account.NetworkManager;
            await AccountChanged();
        });
    }
}