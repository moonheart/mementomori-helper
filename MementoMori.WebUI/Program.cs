using System.Globalization;
using MementoMori;
using MementoMori.Apis;
using MementoMori.Common;
using MementoMori.Jobs;
using MementoMori.Option;
using MementoMori.WebUI.ViewModels;
using MudBlazor.Services;
using MementoMori.WebUI.Extensions;
using Quartz;
using ReactiveUI;
using MementoMori.WebUI;
using MementoMori.WebUI.UI;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Index = MementoMori.BlazorShared.Pages.Index;
using Ortega.Common.Manager;
using MudBlazor;
using Refit;
using MagicOnion;
using Microsoft.Extensions.FileProviders;

internal class Program
{
    public static void Main(string[] args)
    {
        PlatformRegistrationManager.SetRegistrationNamespaces(RegistrationNamespace.Blazor);
        var builder = WebApplication.CreateBuilder(args);

        IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        builder.Services.AddSingleton(physicalProvider);

        builder.Configuration.AddJsonFile(physicalProvider, "appsettings.other.json", true, true);
        builder.Configuration.AddJsonFile(physicalProvider, "appsettings.user.json", true, true);

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

        builder.Services.AddSingleton(sp =>
        {
            var serverUrl = sp.GetRequiredService<IWritableOptions<GameConfig>>().Value.ServerUrl;
            if (string.IsNullOrEmpty(serverUrl)) serverUrl = "https://github.com";
            return RestService.For<IMemeMoriServerApi>(serverUrl);
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