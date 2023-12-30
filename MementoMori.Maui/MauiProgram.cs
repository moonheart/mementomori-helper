using MementoMori.Common;
using MementoMori.Jobs;
using MementoMori.Option;
using MementoMori.WebUI.UI;

using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MudBlazor.Services;

using Quartz;

using System.Globalization;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using MudBlazor;

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
                    o.Dsn = "https://89589ae6d459add80b04ac7f9069f9ee@sentry.moonheartmoon.com/2";
                    o.TracesSampleRate = 1.0;
                    o.AutoSessionTracking = true;
                    o.IsGlobalModeEnabled = true;
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            Directory.SetCurrentDirectory(FileSystem.Current.AppDataDirectory);
            var embeddedFileProvider = new EmbeddedFileProvider(typeof(App).Assembly);

            builder.Configuration.AddJsonFile(embeddedFileProvider, "appsettings.json", false, false);
            builder.Configuration.AddJsonFile("appsettings.user.json", true, true);
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddMudMarkdownServices();

            builder.Services.AddMementoMori();
            builder.Services.AddMementoMoriBlazorShared();
            builder.Services.AddHttpClient();

            builder.Services.AddOptions();
            builder.Services.ConfigureWritable<AuthOption>(builder.Configuration.GetSection("AuthOption"), "appsettings.user.json");
            builder.Services.ConfigureWritable<GameConfig>(builder.Configuration.GetSection("GameConfig"), "appsettings.user.json");
            builder.Services.ConfigureWritable<PlayersOption>(builder.Configuration.GetSection("PlayersOption"), "appsettings.user.json");

            builder.Services.AddQuartz();
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            Services.Setup(app.Services);

            return app;
        }


    }
}
