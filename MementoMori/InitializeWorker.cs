using Microsoft.Extensions.Hosting;

using static MementoMori.Ortega.Common.ClientConst;
using System.Globalization;

namespace MementoMori;

public class InitializeWorker: BackgroundService
{
    private readonly AccountManager _accountManager;
    private readonly MementoNetworkManager _networkManager;

    public InitializeWorker(AccountManager accountManager, MementoNetworkManager networkManager)
    {
        _accountManager = accountManager;
        _networkManager = networkManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _accountManager.MigrateToAccountArray();
        _accountManager.CurrentCulture = CultureInfo.CurrentCulture;
        await _networkManager.Initialize();
        await _networkManager.DownloadMasterCatalog();
        _networkManager.SetCultureInfo(CultureInfo.CurrentCulture);
        await _accountManager.AutoLogin();
    }
}