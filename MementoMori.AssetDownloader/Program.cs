using MementoMori;
using MementoMori.Option;

namespace ConsoleApp1;

internal class Program
{
    private static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddLogging(log => log.ClearProviders().AddSimpleConsole(c =>
        {
            c.SingleLine = true;
            c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
        }));
        builder.Services.AddOptions();
        builder.Services.Configure<DownloaderOption>(builder.Configuration);
        builder.Services.ConfigureWritable<AuthOption>(builder.Configuration.GetSection("Auth"));
        builder.Services.ConfigureWritable<GameConfig>(builder.Configuration.GetSection("GameConfig"));
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
    public string WorkingDir { get; set; }
    public string AListUrl { get; set; }
    public string AlistUsername { get; set; }
    public string AlistPassword { get; set; }
    public string AListTargetPath { get; set; }
    public string ApkVersionFile { get; set; }
    public string ExportAssetType { get; set; } = "tex2d,audio,video,textAsset,sprite";
    
    public bool SkipDownloadFromBoi { get; set; }
    public string[] ForceUploadFiles { get; set; } = [];
}