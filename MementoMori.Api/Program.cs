using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using MementoMori.Api.Import;
using MementoMori.Api.Infrastructure.Database;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
AddUserConfiguration(builder);
builder.Logging.AddProvider(new MementoMori.Api.Infrastructure.FileLoggerProvider());

// UI language for job logs / API messages: "en" (default) or "zh"
var uiCulture = System.Globalization.CultureInfo.GetCultureInfo(builder.Configuration["Localization:Culture"] ?? "en");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = uiCulture;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = uiCulture;
// 游戏文本 (角色名/装备名, 日志里用到) 也跟着这个语言; zh-TW/zh-Hant 用繁体, 其他 zh 用简体
MementoMori.Ortega.Share.Masters.TextResourceTable.SetLanguageType(uiCulture.TwoLetterISOLanguageName switch
{
    "zh" => uiCulture.Name.Contains("TW") || uiCulture.Name.Contains("Hant") ? LanguageType.zhTW : LanguageType.zhCN,
    "ja" => LanguageType.jaJP,
    "ko" => LanguageType.koKR,
    _ => LanguageType.enUS,
});

// Configure FreeSql
var fsql = new FreeSql.FreeSqlBuilder()
    .UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=mementomori.db")
    .UseAutoSyncStructure(true) // 自动同步表结构
    .Build();

builder.Services.AddSingleton<IFreeSql>(fsql);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MementoMori API",
        Version = "v1",
        Description = "MementoMori Game Helper REST API"
    });
});

// Add SignalR
builder.Services.AddSignalR();
builder.Services.AddSingleton<MementoMori.Api.Infrastructure.JobLogger>();

// Add Auto Injection (Injectio)
builder.Services.AddMementoMoriApi();

// Add Quartz
builder.Services.AddQuartz(q =>
{
    // 默认配置
});
builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000", "http://localhost:4173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add HttpClient
builder.Services.AddHttpClient();

// Register Hosted Services (Special Case)
builder.Services.AddHostedService(sp => sp.GetRequiredService<MementoMori.Api.Services.MasterDataService>());
builder.Services.AddHostedService<MementoMori.Api.Autopilot.RewardTracker>();
builder.Services.AddHostedService<MementoMori.Api.Services.AutoLoginService>();

var app = builder.Build();

// 检查是否执行 RPC 契约导出并退出
if (args.Contains("--export-rpc"))
{
    var discoveryService = app.Services.GetRequiredService<MementoMori.Api.Services.OrtegaApiDiscoveryService>();
    var outputDir = Path.Combine(builder.Environment.ContentRootPath, "../web/src/api");
    MementoMori.Api.Infrastructure.RpcManifestGenerator.Generate(discoveryService, outputDir);
    return;
}

// 一次性导入旧 Blazor 配置。只在数据库没有账号时自动检测; 也可用 --import-legacy 手动执行。
try
{
    using var legacyScope = app.Services.CreateScope();
    var legacyImporter = legacyScope.ServiceProvider.GetRequiredService<LegacyConfigImporter>();

    var importArg = args.FirstOrDefault(a => a == "--import-legacy" || a.StartsWith("--import-legacy="));
    if (importArg != null)
    {
        var importIndex = Array.IndexOf(args, "--import-legacy");
        var importPath = importArg.StartsWith("--import-legacy=")
            ? importArg["--import-legacy=".Length..]
            : importIndex >= 0 && importIndex + 1 < args.Length ? args[importIndex + 1] : null;
        importPath ??= legacyImporter.ResolveAutoPath();

        if (string.IsNullOrWhiteSpace(importPath))
        {
            Console.Error.WriteLine("用法: MementoMori.Api --import-legacy <path-to-appsettings.user.json> [--dry-run] [--force]");
            Environment.ExitCode = 1;
            return;
        }

        var importResult = await legacyImporter.ImportAsync(importPath, args.Contains("--force"), args.Contains("--dry-run"));
        Console.WriteLine(importResult.ToSummary());
        if (importResult.Errors.Count > 0) Environment.ExitCode = 1;
        return;
    }

    await legacyImporter.AutoImportAsync();
}
catch (Exception ex)
{
    if (args.Any(a => a.StartsWith("--import-legacy")))
    {
        Console.Error.WriteLine(ex is InvalidOperationException ? ex.Message : ex.ToString());
        Environment.ExitCode = 1;
        return;
    }

    app.Services.GetRequiredService<ILogger<Program>>().LogError(ex, "Legacy config import failed");
}

// 启动时的阻塞式 Master 数据更新
try
{
    using var scope = app.Services.CreateScope();
    var masterDataService = scope.ServiceProvider.GetRequiredService<MementoMori.Api.Services.MasterDataService>();
    // 使用 GetAwaiter().GetResult() 或在顶层使用 await（取决于 Program.cs 是否是异步的）
    // 这里的 Program.cs 隐式支持异步
    await masterDataService.SyncMasterDataAsync();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogCritical(ex, "Failed to perform initial master data sync");
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

// 前端打包后放在 wwwroot, 同一个端口一起提供
app.UseDefaultFiles();
app.UseStaticFiles();

// Add User ID authentication middleware
app.UseMiddleware<MementoMori.Api.Middleware.UserIdAuthenticationMiddleware>();

app.UseAuthorization();
app.MapControllers();

// Map SignalR Hubs
app.MapHub<MementoMori.Api.Infrastructure.JobHub>("/hubs/jobs");
app.MapHub<MementoMori.Api.Infrastructure.LocalRaidHub>("/hubs/localraid");
app.MapFallbackToFile("index.html"); // React 路由 (/dashboard 等)

app.Run();

// 加载用户覆盖配置: appsettings.json 会被新版本覆盖, 但 appsettings.user.json 不会。
// 必须插到环境变量之前, 保证 Docker/.env 仍然能覆盖文件。
static void AddUserConfiguration(WebApplicationBuilder builder)
{
    var candidates = new[]
    {
        Path.Combine(Directory.GetCurrentDirectory(), "appsettings.user.json"),
        Path.Combine(builder.Environment.ContentRootPath, "appsettings.user.json"),
        Path.Combine(AppContext.BaseDirectory, "appsettings.user.json")
    };
    var userConfigPath = candidates
        .Select(Path.GetFullPath)
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .FirstOrDefault(File.Exists);
    if (userConfigPath == null) return;

    var sources = builder.Configuration.Sources;

    // WebApplication.CreateBuilder 已经把环境变量/命令行加到末尾;
    // 先移出来, 添加 user json, 再按原顺序放回去。
    var deferredSources = sources
        .Where(s => s is EnvironmentVariablesConfigurationSource or CommandLineConfigurationSource)
        .ToList();

    foreach (var source in deferredSources)
    {
        sources.Remove(source);
    }

    builder.Configuration.AddJsonFile(userConfigPath, optional: true, reloadOnChange: true);

    foreach (var source in deferredSources)
    {
        sources.Add(source);
    }
}
