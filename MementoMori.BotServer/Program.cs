using BotServer;
using MementoMori.BotServer.Options;
using MementoMori.Option;

namespace MementoMori.BotServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.ConfigureWritable<AuthOption>(context.Configuration.GetSection("Auth"));
                    services.ConfigureWritable<BotOptions>(context.Configuration.GetSection("Bot"));
                    services.ConfigureWritable<GameConfig>(context.Configuration.GetSection("Game"));
                    services.AddHttpClient();
                    services.AddSingleton<MementoNetworkManager>();
                    services.Discover();
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}