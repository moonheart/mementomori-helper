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
        await account.Funcs.ReadAllMemories();
        if (_gameConfig.Value.AutoJob.AutoFreeGacha) await account.Funcs.FreeGacha();
        if (_gameConfig.Value.AutoJob.AutoUseItems) await account.Funcs.AutoUseItems();
        if (_gameConfig.Value.AutoJob.AutoPullGacha) await account.Funcs.AutoPullGacha();
        if (_gameConfig.Value.AutoJob.AutoRankUpWithFodder) await account.Funcs.AutoRankUpSmart();
        if (_gameConfig.Value.AutoJob.AutoBuildTeam) await account.Funcs.AutoBuildTeam();
        if (_gameConfig.Value.AutoJob.AutoLevelUpCharacter) await account.Funcs.AutoLevelUp();
        if (_gameConfig.Value.AutoJob.AutoEquipBest) await account.Funcs.AutoEquipBest();
        if (_gameConfig.Value.AutoJob.AutoReinforceTeamEquipment) await account.Funcs.AutoReinforceTeamEquipment();
        if (_gameConfig.Value.AutoJob.AutoSphere) await account.Funcs.AutoSphere();
        if (_gameConfig.Value.AutoJob.AutoProgressQuest) await account.Funcs.AutoProgressQuest();
        if (_gameConfig.Value.AutoJob.AutoProgressTower) await account.Funcs.AutoClimbTowers();
    }
}