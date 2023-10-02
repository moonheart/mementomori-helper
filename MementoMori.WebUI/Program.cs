using MementoMori;
using MementoMori.Account;
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

        builder.Services.AddSingleton<IFreeSql>(services =>
        {
            IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=D:\Git_Github\MementoMori-release\mementomori_helper.sqlite")
                .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}")) //监听SQL语句
                .Build();
            return fsql;
        });
        builder.Services.AddSingleton<AccountManager>();
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
        
        // app.Services.GetService<AccountManager>().CreateAccount("admin", "123123123");
        app.Services.GetService<MementoNetworkManager>().DownloadMasterCatalog().ConfigureAwait(false).GetAwaiter().GetResult();
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