using MementoMori.Option;
using Quartz;

namespace MementoMori.Jobs;

public class LocalRaidJob : IJob
{
    private AccountManager _accountManager;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public LocalRaidJob(AccountManager accountManager, IWritableOptions<GameConfig> gameConfig)
    {
        _accountManager = accountManager;
        _gameConfig = gameConfig;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.Value.AutoJob.AutoLocalRaid) return;
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
        await account.Funcs.AutoLocalRaid();
    }
}