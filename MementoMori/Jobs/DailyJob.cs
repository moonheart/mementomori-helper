using AutoCtor;

using Quartz;

namespace MementoMori.Jobs;

[AutoConstruct]
public partial class DailyJob: IJob
{
    private readonly AccountManager _accountManager;

    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
        await account.Funcs.ExecuteAllQuickAction();
    }
}