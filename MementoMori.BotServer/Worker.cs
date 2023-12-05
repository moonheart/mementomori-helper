using System.Globalization;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Action;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.MessageMatching;
using MementoMori.BotServer.Plugins;

namespace MementoMori.BotServer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MementoNetworkManager _networkManager;
        private readonly SessionAccessor _sessionAccessor;
        private readonly AdminPlugin _adminPlugin;
        private readonly MementoMoriQueryPlugin _mementoMoriQueryPlugin;

        public Worker(ILogger<Worker> logger,
            MementoNetworkManager networkManager,
            SessionAccessor sessionAccessor,
            AdminPlugin adminPlugin,
            MementoMoriQueryPlugin mementoMoriQueryPlugin)
        {
            _logger = logger;
            _networkManager = networkManager;
            _sessionAccessor = sessionAccessor;
            _adminPlugin = adminPlugin;
            _mementoMoriQueryPlugin = mementoMoriQueryPlugin;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _networkManager.DownloadMasterCatalog();
            _networkManager.SetCultureInfo(CultureInfo.CreateSpecificCulture("zh-CN"));

            _sessionAccessor.Session.UseMessageMatchPlugin(_adminPlugin);
            _sessionAccessor.Session.UseMessageMatchPlugin(_mementoMoriQueryPlugin);

            await _sessionAccessor.Session.StartAsync();

            _logger.LogInformation("start ok");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            await _sessionAccessor.Session.StopAsync();
            await _sessionAccessor.Session.WaitForShutdownAsync();
        }
    }
}