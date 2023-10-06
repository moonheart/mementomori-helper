using MementoMori;

namespace ConsoleApp1;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddCommandLine(args);
        configurationBuilder.AddEnvironmentVariables();
        var configurationRoot = configurationBuilder.Build();

        IServiceCollection services = new ServiceCollection();
        services.AddOptions();
        services.Configure<DownloaderOption>(configurationRoot);
        services.AddLogging(log => log.AddConsole());
        services.AddSingleton<TimeManager>();
        services.AddSingleton<MementoNetworkManager>();
        services.AddSingleton<AssetDownloader>();

        var serviceProvider = services.BuildServiceProvider();

        await serviceProvider.GetRequiredService<AssetDownloader>().StartAsync(CancellationToken.None);

        while (true)
        {
            var i = Console.Read();
            if (i == -1) await Task.Delay(1000);
        }
    }
}

internal class DownloaderOption
{
    public string GameOs { get; set; }
    public string AssetStutioCliPath { get; set; }
    public string DownloadPath { get; set; }
    public string AListUrl { get; set; }
    public string AlistUsername { get; set; }
    public string AlistPassword { get; set; }
    public string AListTargetPath { get; set; }
}