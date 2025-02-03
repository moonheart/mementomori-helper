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
                if (string.IsNullOrEmpty(serverUrl)) serverUrl = "https://github.com";
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