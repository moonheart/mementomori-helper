using System.Collections.ObjectModel;
using System.Globalization;
using MementoMori.Common;
using Microsoft.AspNetCore.Components;

namespace MementoMori.Maui
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Logs = new();
        public MainPage()
        {
            InitializeComponent();
            LogList.ItemsSource = Logs;
            Task.Run(InitializeMemento);
        }

        private void UpdateInfo(string msg)
        {
            Logs.Add(msg);
            this.Dispatcher.DispatchAsync(() => { LogList.ScrollTo(msg, ScrollToPosition.MakeVisible, true); });
        }

        private async Task InitializeMemento()
        {
            var accountManager = Common.Services.Get<AccountManager>();
            var networkManager = Common.Services.Get<MementoNetworkManager>();
            UpdateInfo("Initializing...");
            accountManager.MigrateToAccountArray();
            accountManager.CurrentCulture = CultureInfo.CurrentCulture;
            await networkManager.Initialize(UpdateInfo);
            UpdateInfo("Downloading master catalog...");
            await networkManager.DownloadMasterCatalog(UpdateInfo);
            UpdateInfo("Loading master catalog...");
            networkManager.SetCultureInfo(CultureInfo.CurrentCulture);
            UpdateInfo("Auto login...");
            await accountManager.AutoLogin();
            // return;
            Logs.Clear();

            Dispatcher.Dispatch(() =>
            {
                LogList.IsVisible = false;
                blazorWebView.IsVisible = true;
            });

            await blazorWebView.TryDispatchAsync(sp =>
            {
                var navi = sp.GetRequiredService<NavigationManager>();
                navi.NavigateTo(navi.Uri, true);
            });
            Dispatcher.Dispatch(() =>
            {
                blazorWebView.Focus();
            });
        }
    }
}