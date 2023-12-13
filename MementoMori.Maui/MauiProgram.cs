using MementoMori.Common;
using MementoMori.Jobs;
using MementoMori.Option;
using MementoMori.WebUI.UI;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MudBlazor.Services;

using Quartz;

using System.Globalization;

namespace MementoMori.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Configuration.AddJsonFile("appsettings.other.json", true, true);
            builder.Configuration.AddJsonFile("appsettings.user.json", true, true);
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

            builder.Services.AddSingleton<AtlasManager>();
            builder.Services.AddSingleton<AccountManager>();
            builder.Services.AddTransient<MementoNetworkManager>();
            builder.Services.AddTransient<MementoMoriFuncs>();
            builder.Services.AddSingleton<TimeZoneAwareJobRegister>();
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

            app.Services.GetRequiredService<AccountManager>().MigrateToAccountArray();
            app.Services.GetRequiredService<AccountManager>().CurrentCulture = CultureInfo.CurrentCulture;
            Init(app);
            // Task.Run(async () =>
            // {
            //     await app.Services.GetRequiredService<MementoNetworkManager>().DownloadMasterCatalog();
            //     app.Services.GetRequiredService<MementoNetworkManager>().SetCultureInfo(CultureInfo.CurrentCulture);
            //     await app.Services.GetRequiredService<AccountManager>().AutoLogin();
            // });
            return app;
        }

        private static void Init(MauiApp app)
        {
            app.Services.GetRequiredService<MementoNetworkManager>().DownloadMasterCatalog();
            // app.Services.GetRequiredService<MementoNetworkManager>().SetCultureInfo(CultureInfo.CurrentCulture);
            // await app.Services.GetRequiredService<AccountManager>().AutoLogin();
        }
    }
}
