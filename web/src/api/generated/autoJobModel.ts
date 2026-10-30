/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

export class AutoJobModel {
    disableAll: boolean;
    autoPvp: boolean;
    autoLegendLeague: boolean;
    autoDungeonBattle: boolean;
    autoUseItems: boolean;
    autoFreeGacha: boolean;
    autoRankUpCharacter: boolean;
    autoOpenGuildRaid: boolean;
    autoLocalRaid: boolean;
    autoDeployGuildDefense: boolean;
    autoChangeGachaRelic: boolean;
    autoDrawGachaRelic: boolean;
    autoBuyShopItem: boolean;
    autoReceiveAutoBattleReward: boolean;
    autoBossQuickBattle: boolean;
    autoBossHighSpeedBattle: boolean;
    autoBountyQuestDispatch: boolean;
    autoBountyQuestReward: boolean;
    dailyJobCron: string = "0 50 4 ? * *";
    hourlyJobCron: string = "0 30 0,4,8,12,16,20 ? * *";
    pvpJobCron: string = "0 0 20 ? * *";
    legendLeagueJobCron: string = "0 10 20 ? * *";
    guildRaidBossReleaseCron: string = "0 0 * ? * *";
    autoBuyShopItemJobCron: string = "0 9 9,12,15,18 ? * *";
    autoLocalRaidJobCron: string = "0 31 12,19 ? * *";
    autoDeployGuildDefenseJobCron: string = "0 20 19 ? * *";
    autoChangeGachaRelicJobCron: string = "0 40 4 ? * MON *";
    autoDrawGachaRelicJobCron: string = "0 0 6 ? * SUN *";
    disabledDailyActions: string[] = [];
    disabledHourlyActions: string[] = [];
}
