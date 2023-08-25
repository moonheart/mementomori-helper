using MementoMori;
using MementoMori.Common;
using MementoMori.WebUI.Data;
using MementoMori.WebUI.ViewModels;
using MudBlazor.Services;
using ReactiveUI;

internal class Program
{
    public static void Main(string[] args)
    {
        ReactiveUI.PlatformRegistrationManager.SetRegistrationNamespaces(RegistrationNamespace.Blazor);
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.dev.json", true, true);

        builder.Services.AddMudServices();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<MementoMoriFuncs>();
        builder.Services.AddSingleton<CharactorUnitViewModel>();

        builder.Services.AddOptions();
        builder.Services.Configure<AuthOption>(builder.Configuration.GetSection("AuthOption"));
        builder.Services.Configure<GameConfig>(builder.Configuration.GetSection("GameConfig"));

        var app = builder.Build();
        
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