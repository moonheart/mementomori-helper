using Quartz;

namespace MementoMori.Jobs;

[DisallowConcurrentExecution]
[Cron("0 20 4 ? * *")]
public class DailyJob: IJob
{
    private MementoMoriFuncs _mementoMoriFuncs;

    public DailyJob(MementoMoriFuncs mementoMoriFuncs)
    {
        _mementoMoriFuncs = mementoMoriFuncs;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (_mementoMoriFuncs.IsQuickActionExecuting)
        {
            return;
        }
        await _mementoMoriFuncs.Login();
        await _mementoMoriFuncs.ExecuteAllQuickAction();
    }
}