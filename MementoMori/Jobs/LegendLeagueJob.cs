using MementoMori.Option;
using Quartz;

namespace MementoMori.Jobs;

public class LegendLeagueJob : IJob
{
    private AccountManager _accountManager;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public LegendLeagueJob(AccountManager accountManager, IWritableOptions<GameConfig> gameConfig)
    {
        _accountManager = accountManager;
        _gameConfig = gameConfig;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.Value.AutoJob.AutoLegendLeague) return;

        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();

        await account.Funcs.LegendLeagueAuto();
    }
}