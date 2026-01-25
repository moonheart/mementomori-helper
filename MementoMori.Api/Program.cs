using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using MementoMori.Api.Infrastructure.Database;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

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
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Add HttpClient
builder.Services.AddHttpClient();

// Register infrastructure services
builder.Services.AddSingleton<MementoMori.Api.Services.VersionService>();
builder.Services.AddSingleton<MementoMori.Api.Services.MasterDataService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<MementoMori.Api.Services.MasterDataService>());
builder.Services.AddSingleton<MementoMori.Api.Infrastructure.NetworkManager>();

// Register Ortega proxy services
builder.Services.AddSingleton<MementoMori.Api.Services.OrtegaApiDiscoveryService>();
builder.Services.AddScoped<MementoMori.Api.Infrastructure.OrtegaInvoker>();

// Register account manager (Singleton)
builder.Services.AddSingleton<MementoMori.Api.Services.AccountManager>();

// Register application services
builder.Services.AddScoped<MementoMori.Api.Services.AccountCredentialService>();
builder.Services.AddScoped<MementoMori.Api.Services.AccountService>();
builder.Services.AddScoped<MementoMori.Api.Services.MissionService>();
builder.Services.AddScoped<MementoMori.Api.Services.LocalizationService>();
builder.Services.AddScoped<MementoMori.Api.Services.PlayerSettingService>();
builder.Services.AddSingleton<MementoMori.Api.Services.JobManagerService>();
builder.Services.AddSingleton<MementoMori.Api.Services.GameActionService>();

var app = builder.Build();

// 检查是否执行 RPC 契约导出并退出
if (args.Contains("--export-rpc"))
{
    var discoveryService = app.Services.GetRequiredService<MementoMori.Api.Services.OrtegaApiDiscoveryService>();
    var outputDir = Path.Combine(builder.Environment.ContentRootPath, "../../src/api");
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

// Add User ID authentication middleware
app.UseMiddleware<MementoMori.Api.Middleware.UserIdAuthenticationMiddleware>();

app.UseAuthorization();
app.MapControllers();

// Map SignalR Hubs
app.MapHub<MementoMori.Api.Infrastructure.JobHub>("/hubs/jobs");

app.Run();
