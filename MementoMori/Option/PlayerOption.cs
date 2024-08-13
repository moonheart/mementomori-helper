namespace MementoMori;

public class PlayerOption
{
    public long PlayerId { get; set; }
    public PvpOption BattleLeague { get; set; } = new();
    public PvpOption LegendLeague { get; set; } = new();
    public GameConfig.LocalRaidConfig LocalRaid { get; set; } = new();

    public FriendManageOption FriendManage { get; set; } = new();

    public GuildTowerOption GuildTower { get; set; } = new();

    public BountyQuestOption BountyQuest { get; set; } = new();

    public GameConfig.GachaConfigModel GachaConfig { get; set; } = new();

    public Dictionary<QuickActionType, bool> QuickActionSwitch { get; set; } = new()
    {
        {QuickActionType.DailyCheckIn, true},
        {QuickActionType.ReceiveVipGift, true},
        {QuickActionType.ReceiveMonthlyBoost, true},
        {QuickActionType.ReceiveAutoBattleReward, true},
        {QuickActionType.ReceiveFriendPoint, true},
        {QuickActionType.ReceiveInbox, true},
        {QuickActionType.ReinforcementEquipmentOnce, true},
        {QuickActionType.BossBattle, true},
        {QuickActionType.TowerBattle, true},
        {QuickActionType.BattleLeague, false},
        {QuickActionType.LegendLeague, false},
        {QuickActionType.BattleHighSpeed, true},
        {QuickActionType.FreeGacha, true},
        {QuickActionType.GuildCheckin, true},
        {QuickActionType.GuildRaid, true},
        {QuickActionType.GuildOpenBoss, true},
        {QuickActionType.ReceiveGvgReward, true},
        {QuickActionType.LocalRaid, false},
        {QuickActionType.BountyQuestReceive, true},
        {QuickActionType.BountyQuestDispatch, true},
        {QuickActionType.BuyShopItem, true},
        {QuickActionType.UseItem, true},
        {QuickActionType.RankUpCharacter, true},
        {QuickActionType.ReceiveAchievementReward, true}
    };
}

public class BountyQuestOption
{
    /// <summary>
    ///     the number of refreshes allowed for the bounty quest.
    /// </summary>
    public int MaxRefreshCount { get; set; }

    /// <summary>
    ///     the number of total refresh count of today.
    /// </summary>
    public Dictionary<string, int> TodayRefreshCount { get; set; } = [];
}

public class GuildTowerOption
{
    public bool AutoEntry { get; set; }
    public bool AutoChallenge { get; set; }
    public int AutoChallengeRetryCount { get; set; } = 10;
    public bool AutoReinforcement { get; set; }
    public bool AutoReceiveReward { get; set; }
}

public class FriendManageOption
{
    public bool AutoRemoveInactiveFriend { get; set; }

    public List<long> AutoRemoveWhitelist { get; set; } = [];

    public bool AutoSendFriendRequest { get; set; }

    public bool AutoAcceptFriendRequest { get; set; }
}