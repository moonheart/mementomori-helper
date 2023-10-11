using MementoMori.Option;

using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

[DisallowConcurrentExecution]
public class HourlyJob : IJob
{
    private MementoMoriFuncs _mementoMoriFuncs;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public HourlyJob(MementoMoriFuncs mementoMoriFuncs, IWritableOptions<GameConfig> gameConfig)
    {
        _mementoMoriFuncs = mementoMoriFuncs;
        _gameConfig = gameConfig;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (_mementoMoriFuncs.IsQuickActionExecuting)
        {
            return;
        }

        await _mementoMoriFuncs.Login();
        await _mementoMoriFuncs.BountyQuestStartAuto();
        await _mementoMoriFuncs.PresentReceiveItem();
        await _mementoMoriFuncs.GetAutoBattleReward();
        await _mementoMoriFuncs.GuildRaid();
        await _mementoMoriFuncs.BulkTransferFriendPoint();
        await _mementoMoriFuncs.BountyQuestRewardAuto();
        await _mementoMoriFuncs.CompleteMissions();
        await _mementoMoriFuncs.RewardMissonActivity();
        if (_gameConfig.Value.AutoJob.AutoFreeGacha) await _mementoMoriFuncs.FreeGacha();
        if (_gameConfig.Value.AutoJob.AutoUseItems) await _mementoMoriFuncs.AutoUseItems();
    }
}