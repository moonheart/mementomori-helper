using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using MementoMori.Api.Infrastructure.Database;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

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
