using MementoMori.WebUI.Extensions;
using Quartz;

namespace MementoMori.WebUI.Jobs;

[DisallowConcurrentExecution]
[Cron("0 30 3,7,15,19,23 ? * *")]
public class HourlyJob: IJob
{
    private MementoMoriFuncs _mementoMoriFuncs;

    public HourlyJob(MementoMoriFuncs mementoMoriFuncs)
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
        await _mementoMoriFuncs.PresentReceiveItem();
        await _mementoMoriFuncs.GetAutoBattleReward();
        await _mementoMoriFuncs.GuildRaid();
        await _mementoMoriFuncs.BulkTransferFriendPoint();
        await _mementoMoriFuncs.BountyQuestRewardAuto();
        await _mementoMoriFuncs.CompleteMissions();
        await _mementoMoriFuncs.RewardMissonActivity();
        await _mementoMoriFuncs.FreeGacha();
        await _mementoMoriFuncs.AutoUseItems();
    }
}