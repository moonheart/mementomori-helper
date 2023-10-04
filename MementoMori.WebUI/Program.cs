using System.Globalization;
using MementoMori;
using MementoMori.Common;
using MementoMori.Jobs;
using MementoMori.WebUI.ViewModels;
using MudBlazor.Services;
using MementoMori.WebUI.Extensions;
using Quartz;
using ReactiveUI;

internal class Program
{
    public static void Main(string[] args)
    {
        PlatformRegistrationManager.SetRegistrationNamespaces(RegistrationNamespace.Blazor);
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.user.json", true, true);

        builder.Services.AddMudServices();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<TimeManager>();
        builder.Services.AddSingleton<MementoNetworkManager>();
        builder.Services.AddSingleton<MementoMoriFuncs>();
        builder.Services.AddSingleton<TimeZoneAwareJobRegister>();

        builder.Services.AddOptions();
        builder.Services.Configure<AuthOption>(builder.Configuration.GetSection("AuthOption"));
        builder.Services.Configure<GameConfig>(builder.Configuration.GetSection("GameConfig"));
        
        builder.Services.AddQuartz();
        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        var app = builder.Build();
        
        app.Services.GetService<MementoNetworkManager>().DownloadMasterCatalog(CultureInfo.CurrentCulture).ConfigureAwait(false).GetAwaiter().GetResult();
        Services.Setup(app.Services);

// Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}