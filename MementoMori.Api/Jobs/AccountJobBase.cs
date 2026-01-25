using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 基础 Job 类，提供通用的账户获取逻辑
/// </summary>
public abstract class AccountJobBase : IJob
{
    protected readonly AccountManager AccountManager;
    protected readonly ILogger Logger;
    protected readonly IServiceProvider ServiceProvider;

    protected AccountJobBase(AccountManager accountManager, ILogger logger, IServiceProvider serviceProvider)
    {
        AccountManager = accountManager;
        Logger = logger;
        ServiceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0)
        {
            Logger.LogWarning("Job {JobKey} executed without valid userId", context.JobDetail.Key);
            return;
        }

        var jobLogger = ServiceProvider.GetRequiredService<JobLogger>();

        try
        {
            // 1. 获取账户上下文
            var accountContext = await AccountManager.GetOrCreateAsync(userId);

            // 2. 随机抖动 (Jitter) - 仅针对定时触发的任务，手动触发的可以不抖动
            if (context.Trigger.Key.Name.Contains("trigger"))
            {
                await RandomJitterAsync(userId, jobLogger);
            }

            // 3. 确保已登录（如果没有有效 Session 则尝试静默登录）
            await EnsureLoggedInAsync(accountContext, jobLogger);

            // 4. 执行业务逻辑
            await ExecuteAsync(context, accountContext);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error executing job {JobType} for user {UserId}", GetType().Name, userId);
            await jobLogger.LogAsync(userId, $"任务执行异常: {ex.Message}", "Error");
        }
    }

    protected virtual async Task RandomJitterAsync(long userId, JobLogger jobLogger)
    {
        var random = new Random();
        var delaySeconds = random.Next(5, 31); // 5-30秒随机，避免并发峰值
        Logger.LogInformation("Job {JobType} for user {UserId} will start after jitter delay of {Delay}s", GetType().Name, userId, delaySeconds);
        // await jobLogger.LogAsync(userId, $"等待执行抖动 ({delaySeconds}s)...");
        await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
    }

    protected virtual async Task EnsureLoggedInAsync(AccountContext accountContext, JobLogger jobLogger)
    {
        // TODO: 完善 Session 有效性检查。目前仅检查内存状态
        if (accountContext.AccountInfo.IsLoggedIn)
        {
            return;
        }

        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Job {JobType} for user {UserId} triggered auto-login", GetType().Name, userId);
        await jobLogger.LogAsync(userId, "检测到登录失效，尝试自动重登录...");

        using var scope = ServiceProvider.CreateScope();
        var accountService = scope.ServiceProvider.GetRequiredService<AccountService>();
        
        var loginResp = await accountService.LoginAsync(userId, accountContext.AccountInfo.ClientKey);
        
        if (!loginResp.Success)
        {
            throw new Exception($"自动重登录失败: {loginResp.Message}");
        }

        await jobLogger.LogAsync(userId, "自动重登录成功。");
    }

    protected abstract Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext);
}
