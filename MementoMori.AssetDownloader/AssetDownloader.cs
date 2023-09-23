using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori;

namespace ConsoleApp1;

internal class AssetDownloader : BackgroundService
{
    private readonly MementoNetworkManager _networkManager;
    private const string GameOs = "Android";

    public AssetDownloader(MementoNetworkManager networkManager)
    {
        _networkManager = networkManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _networkManager.DownloadAssets(stoppingToken);
            await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
        }
    }
}