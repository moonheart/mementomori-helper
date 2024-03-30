using System.Globalization;
using AutoCtor;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.Action;
using EleCho.GoCqHttpSdk.Message;
using EleCho.GoCqHttpSdk.MessageMatching;
using MementoMori.BotServer.Plugins;

namespace MementoMori.BotServer
{
    [AutoConstruct]
    public partial class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MementoNetworkManager _networkManager;
        private readonly SessionAccessor _sessionAccessor;
        private readonly MementoMoriQueryPlugin _mementoMoriQueryPlugin;
        private readonly AdminPlugin _adminPlugin;


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _networkManager.DownloadMasterCatalog();
            _networkManager.SetCultureInfo(CultureInfo.CreateSpecificCulture("zh-CN"));
            _sessionAccessor.OnConnected = session =>
            {
                session.UseMessageMatchPlugin(_adminPlugin);
                session.UseMessageMatchPlugin(_mementoMoriQueryPlugin);
            };

            while (!_sessionAccessor.Session.IsConnected)
            {
                await Task.Delay(100, stoppingToken);
            }
            _logger.LogInformation("start ok");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                if (!_sessionAccessor.Session.IsConnected)
                {
                    _logger.LogWarning("session not connected");
                }
            }

            await _sessionAccessor.Session.StopAsync();
            await _sessionAccessor.Session.WaitForShutdownAsync();
        }
    }
}