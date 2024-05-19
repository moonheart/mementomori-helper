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
using Sentry;
using MementoMori.WebUI.UI;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Net.Http.Headers;
using Index = MementoMori.BlazorShared.Pages.Index;
using Ortega.Common.Manager;
using MudBlazor;

internal class Program
{
    public static void Main(string[] args)
    {
        PlatformRegistrationManager.SetRegistrationNamespaces(RegistrationNamespace.Blazor);
        SentrySdk.ConfigureScope(scope =>
        {
            scope.User = new SentryUser()
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
        builder.Services.AddMudMarkdownServices();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddMementoMori();
        builder.Services.AddMementoMoriBlazorShared();
        builder.Services.AddMementoMoriWebUI();
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

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");

        app.UseStaticFiles();
        app.UseAntiforgery();
        app.MapRazorComponents<App>()
            .AddAdditionalAssemblies(typeof(Index).Assembly)
            .AddInteractiveServerRenderMode();
        app.UseSentryTracing();
        //app.UseRouting();

        //app.MapBlazorHub();
        //app.MapFallbackToPage("/_Host");

        InitializeAsync(app.Services).ConfigureAwait(false).GetAwaiter().GetResult();
        app.Run();
    }

    private static async Task InitializeAsync(IServiceProvider sp)
    {
        var accountManager = sp.GetRequiredService<AccountManager>();
        var networkManager = sp.GetRequiredService<MementoNetworkManager>();
        accountManager.MigrateToAccountArray();
        accountManager.CurrentCulture = CultureInfo.CurrentCulture;
        await networkManager.Initialize();
        await networkManager.DownloadMasterCatalog();
        networkManager.SetCultureInfo(CultureInfo.CurrentCulture);
        await accountManager.AutoLogin();
    }
}