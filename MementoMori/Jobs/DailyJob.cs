using Quartz;

namespace MementoMori.Jobs;

public class DailyJob: IJob
{
    private AccountManager _accountManager;

    public DailyJob(AccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
        await account.Funcs.ExecuteAllQuickAction();
    }
}