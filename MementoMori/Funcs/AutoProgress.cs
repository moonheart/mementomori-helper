using MementoMori.Common.Localization;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    // 一键 / 每小时: 抽卡 → 升星 → 组队 → 成长 → 主线 → 塔 → 再成长, 最后记一个快照
    public async Task AutoProgress()
    {
        var job = GameConfig.AutoJob;
        if (job.AutoPullGacha) await RunTask(ResourceStrings.GachaAutoPull, AutoPullGacha);
        if (job.AutoRankUpWithFodder) await RunTask(ResourceStrings.AutoRankUpFodderButton, AutoRankUpSmart);
        if (job.AutoBuildTeam) await RunTask(ResourceStrings.AutoBuildTeamButton, AutoBuildTeam);
        await AutoGrowAll();
        if (job.AutoProgressQuest) await RunTask(ResourceStrings.AutoClimbQuestButton, AutoProgressQuest);
        if (job.AutoProgressTower) await RunTask(ResourceStrings.AutoClimbTowersButton, AutoClimbTowers);
        // 打完可能拿到了新装备/经验, 再成长一次
        if (job.AutoProgressQuest || job.AutoProgressTower) await AutoGrowAll();
        TakeSnapshot();
    }

    private async Task AutoGrowAll()
    {
        var job = GameConfig.AutoJob;
        if (job.AutoLevelUpCharacter) await RunTask(ResourceStrings.AutoLevelUpButton, AutoLevelUp);
        if (job.AutoEquipBest) await RunTask(ResourceStrings.AutoEquipBest, AutoEquipBest);
        if (job.AutoReinforceTeamEquipment) await RunTask(ResourceStrings.AutoReinforceTeamGear, AutoReinforceTeamEquipment);
        if (job.AutoSphere) await RunTask(ResourceStrings.AutoRunesButton, AutoSphere);
    }
}
