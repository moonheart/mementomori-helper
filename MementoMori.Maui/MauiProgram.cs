using MementoMori.Option;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Quartz;
using Microsoft.Extensions.FileProviders;
using MudBlazor;
using MementoMori.Apis;
using Refit;

namespace MementoMori.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSentry(o =>
                {
                    o.Dsn = "https://89589ae6d459add80b04ac7f9069f9ee@sentry.moonheart.dev/2";
                    o.TracesSampleRate = 1.0;
                    o.AutoSessionTracking = true;
                    o.IsGlobalModeEnabled = true;
                })
                .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

            Directory.SetCurrentDirectory(FileSystem.Current.AppDataDirectory);
            IFileProvider fileProvider = new PhysicalFileProvider(FileSystem.Current.AppDataDirectory);
            builder.Services.AddSingleton(fileProvider);

            builder.Configuration.AddJsonFile(new EmbeddedFileProvider(typeof(App).Assembly), "appsettings.json", false, false);
            builder.Configuration.AddJsonFile(fileProvider, "appsettings.user.json", true, true);
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddMudMarkdownServices();

            builder.Services.AddMementoMori();
            builder.Services.AddMementoMoriBlazorShared();
            builder.Services.AddMementoMoriMaui();
            builder.Services.AddHttpClient();

            builder.Services.AddOptions();
            builder.Services.ConfigureWritable<AuthOption>(builder.Configuration.GetSection("AuthOption"), "appsettings.user.json");
            builder.Services.ConfigureWritable<GameConfig>(builder.Configuration.GetSection("GameConfig"), "appsettings.user.json");
            builder.Services.ConfigureWritable<PlayersOption>(builder.Configuration.GetSection("PlayersOption"), "appsettings.user.json");

            builder.Services.AddSingleton(sp =>
            {
                var serverUrl = sp.GetRequiredService<IWritableOptions<GameConfig>>().Value.ServerUrl;
                return RestService.For<IMemeMoriServerApi>(serverUrl);
            });

            builder.Services.AddQuartz();
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            Common.Services.Setup(app.Services);

            return app;
        }
    }
}