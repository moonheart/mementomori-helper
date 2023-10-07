using MementoMori;

namespace ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddLogging(log => log.AddSimpleConsole(c => c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] "));
        builder.Services.AddOptions();
        builder.Services.Configure<DownloaderOption>(builder.Configuration);
        builder.Services.AddSingleton<TimeManager>();
        builder.Services.AddSingleton<MementoNetworkManager>();
        builder.Services.AddHostedService<AssetDownloader>();

        builder.Build().Run();
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