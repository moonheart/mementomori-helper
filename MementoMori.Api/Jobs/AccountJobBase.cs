using Quartz;
using MementoMori.Api.Services;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 基础 Job 类，提供通用的账户获取逻辑
/// </summary>
public abstract class AccountJobBase : IJob
{
    protected readonly AccountManager AccountManager;
    protected readonly ILogger Logger;

    protected AccountJobBase(AccountManager accountManager, ILogger logger)
    {
        AccountManager = accountManager;
        Logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0)
        {
            Logger.LogWarning("Job {JobKey} executed without valid userId", context.JobDetail.Key);
            return;
        }

        try
        {
            // 获取账户上下文
            var accountContext = await AccountManager.GetOrCreateAsync(userId);
            
            // 确保已登录（如果没有有效 Session 则尝试静默登录）
            if (!accountContext.AccountInfo.IsLoggedIn)
            {
                Logger.LogInformation("Job {JobType} for user {UserId} triggered login", GetType().Name, userId);
                // 这里调用 AccountService 的登录逻辑，或者直接通过 NetworkManager 登录
                // 暂时简单记录日志，后续完善自动登录保护
            }

            await ExecuteAsync(context, accountContext);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error executing job {JobType} for user {UserId}", GetType().Name, userId);
        }
    }

    protected abstract Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext);
}
