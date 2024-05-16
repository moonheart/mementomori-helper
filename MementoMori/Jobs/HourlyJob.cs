using AutoCtor;
using MementoMori.Option;
using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

[AutoConstruct]
public partial class HourlyJob : IJob
{
    private readonly IWritableOptions<GameConfig> _gameConfig;
    private readonly AccountManager _accountManager;


    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
        await account.Funcs.GetLoginBonus();
        await account.Funcs.BountyQuestStartAuto();
        await account.Funcs.PresentReceiveItem();
        await account.Funcs.GetAutoBattleReward();
        await account.Funcs.GuildRaid();
        await account.Funcs.AutoGuildTower();
        await account.Funcs.ReceiveGvgReward();
        await account.Funcs.BulkTransferFriendPoint();
        await account.Funcs.BountyQuestRewardAuto();
        await account.Funcs.CompleteMissions();
        await account.Funcs.RewardMissonActivity();
        if (_gameConfig.Value.AutoJob.AutoFreeGacha) await account.Funcs.FreeGacha();
        if (_gameConfig.Value.AutoJob.AutoUseItems) await account.Funcs.AutoUseItems();
    }
}