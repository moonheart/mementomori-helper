using MementoMori.Api.Infrastructure.Database;

namespace MementoMori.Api.Services;

/// <summary>
/// 启动时把所有保存的账号重新登录一遍 (全新登录, 不复用旧 token), 登录后会注册定时任务.
/// 这样重启/部署后不用手动点 "登录", 定时任务也会接着跑.
/// </summary>
public class AutoLoginService : BackgroundService
{
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(30);
    private const int Attempts = 5;

    private readonly IServiceProvider _services;
    private readonly IFreeSql _db;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AutoLoginService> _logger;

    public AutoLoginService(IServiceProvider services, IFreeSql db, IConfiguration configuration, ILogger<AutoLoginService> logger)
    {
        _services = services;
        _db = db;
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        // 本地开发时别跟服务器上的同一账号抢 session
        if (!_configuration.GetValue("AutoLogin:Enabled", true)) return;

        var accounts = await _db.Select<AccountEntity>().Where(a => a.ClientKey != "").ToListAsync(ct);
        // AutoLogin == false: 旧配置明确关闭了自动登录, 等用户手动登录
        foreach (var account in accounts.Where(a => a.AutoLogin != false))
        {
            for (var attempt = 1; attempt <= Attempts && !ct.IsCancellationRequested; attempt++)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var result = await scope.ServiceProvider.GetRequiredService<AccountService>().LoginAsync(account.UserId, account.ClientKey);
                    if (result.Success)
                    {
                        _logger.LogInformation("Auto-login ok for {UserId} ({Name})", account.UserId, account.Name);
                        break;
                    }

                    _logger.LogWarning("Auto-login attempt {Attempt} for {UserId} failed: {Message}", attempt, account.UserId, result.Message);
                }
                catch (Exception e)
                {
                    // 刚启动时版本/Master 可能还没准备好, 等一会再试
                    _logger.LogWarning(e, "Auto-login attempt {Attempt} for {UserId} threw", attempt, account.UserId);
                }

                await Task.Delay(RetryDelay, ct);
            }
        }
    }
}
