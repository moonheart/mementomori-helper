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
        foreach (var (_, account) in _accountManager.GetAll())
        {
            if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
            await account.Funcs.ExecuteAllQuickAction();
        }
    }
}