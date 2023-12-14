using System.Globalization;
using MementoMori.Common;
using Microsoft.AspNetCore.Components;

namespace MementoMori.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            _ = InitializeMemento();
        }

        private void UpdateInfo(string msg)
        {
            this.Dispatcher.DispatchAsync(() => { infoLabel.Text = msg; });
        }

        private async Task InitializeMemento()
        {
            var accountManager = Services.Get<AccountManager>();
            var networkManager = Services.Get<MementoNetworkManager>();
            UpdateInfo("Initializing...");
            accountManager.MigrateToAccountArray();
            accountManager.CurrentCulture = CultureInfo.CurrentCulture;
            await networkManager.Initialize();
            UpdateInfo("Downloading master catalog...");
            await networkManager.DownloadMasterCatalog();
            networkManager.SetCultureInfo(CultureInfo.CurrentCulture);
            UpdateInfo("Auto login...");
            await accountManager.AutoLogin();
            infoLabel.IsVisible = false;
            blazorWebView.IsVisible = true;
            await blazorWebView.TryDispatchAsync(sp =>
            {
                var navi = sp.GetRequiredService<NavigationManager>();
                navi.NavigateTo(navi.Uri, true);
            });
            blazorWebView.Focus();
        }
    }
}