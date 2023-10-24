using Microsoft.AspNetCore.Components;
using ReactiveUI;

namespace MementoMori.WebUI.Models;

public class AccountComponent : ComponentBase
{
    [Inject]
    public AccountManager AccountManager { get; set; }

    protected AccountInfo AccountInfo;
    protected MementoMoriFuncs Funcs;
    protected MementoNetworkManager NetworkManager;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        // Account = AccountManager.Current;
        AccountManager.WhenAnyValue(d => d.CurrentUserId).Subscribe(userId =>
        {
            var account = AccountManager.Get(userId);
            AccountInfo = account.AccountInfo;
            Funcs = account.Funcs;
            NetworkManager = account.NetworkManager;
            InvokeAsync(() => StateHasChanged());
        });
    }
}