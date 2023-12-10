using System.Globalization;
using MementoMori;
using MementoMori.Common;
using MementoMori.Jobs;
using MementoMori.Option;
using MementoMori.WebUI.ViewModels;
using MudBlazor.Services;
using MementoMori.WebUI.Extensions;
using Quartz;
using ReactiveUI;
using MementoMori.WebUI;
using MementoMori.WebUI.Pages;
using Sentry;
using MementoMori.WebUI.UI;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;

internal class Program
{
    public static void Main(string[] args)
    {
        PlatformRegistrationManager.SetRegistrationNamespaces(RegistrationNamespace.Blazor);
        SentrySdk.ConfigureScope(scope =>
        {
            scope.User = new User()
            {
                IpAddress = "{{auto}}"
            };
        });
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseSentry(o =>
        {
            o.Dsn = "https://89589ae6d459add80b04ac7f9069f9ee@sentry.moonheartmoon.com/2";
            o.TracesSampleRate = 1.0;
            o.AutoSessionTracking = true;
            o.IsGlobalModeEnabled = true;
        });

        builder.Configuration.AddJsonFile("appsettings.other.json", true, true);
        builder.Configuration.AddJsonFile("appsettings.user.json", true, true);

        builder.Services.AddMudServices();

        // builder.Services.AddRazorComponents()
        //     .AddInteractiveServerComponents();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
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
        builder.Services.Configure<StaticFileOptions>(opt =>
        {
            opt.HttpsCompression = HttpsCompressionMode.Compress;
            opt.OnPrepareResponse = ctx =>
            {
                var typedHeaders = ctx.Context.Response.GetTypedHeaders();
                typedHeaders.CacheControl = new CacheControlHeaderValue()
                {
                    Public = true,
                    MaxAge = TimeSpan.FromDays(1)
                };
            };
        });

        builder.Services.AddQuartz();
        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        var app = builder.Build();
        Services.Setup(app.Services);

        app.Services.GetRequiredService<AccountManager>().MigrateToAccountArray();
        app.Services.GetRequiredService<AccountManager>().CurrentCulture = CultureInfo.CurrentCulture;
        app.Services.GetRequiredService<MementoNetworkManager>().DownloadMasterCatalog().ConfigureAwait(false).GetAwaiter().GetResult();
        app.Services.GetRequiredService<MementoNetworkManager>().SetCultureInfo(CultureInfo.CurrentCulture);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");

        app.UseStaticFiles();
        // app.UseAntiforgery();
        // app.MapRazorComponents<App>()
        //     .AddInteractiveServerRenderMode();

        app.UseSentryTracing();
        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Services.GetRequiredService<AccountManager>().AutoLogin().ConfigureAwait(false).GetAwaiter().GetResult();

        app.Run();
    }
}