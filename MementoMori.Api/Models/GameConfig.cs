using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

public class GameConfig
{
    public enum AutoUseItemType
    {
        Others,
        DiamondBag,
        MysteryRune,
        WitchLetterR,
        WitchLetterSr,
        WitchShard,
        Pot,
        Amphora,
        SealedChest,
        WitchLetterGift,
        ChestOfChance
    }

    [ExportTsClass]
    public class AutoJobModel
    {
        public bool DisableAll { get; set; }
        public bool AutoPvp { get; set; }
        public bool AutoLegendLeague { get; set; }
        public bool AutoDungeonBattle { get; set; }
        public bool AutoUseItems { get; set; }
        public bool AutoFreeGacha { get; set; }
        public bool AutoRankUpCharacter { get; set; }
        public bool AutoOpenGuildRaid { get; set; }
        public bool AutoLocalRaid { get; set; }
        public bool AutoDeployGuildDefense { get; set; }
        public bool AutoChangeGachaRelic { get; set; }
        public bool AutoDrawGachaRelic { get; set; }
        public bool AutoBuyShopItem { get; set; }
        public bool AutoReceiveAutoBattleReward { get; set; }
        public bool AutoBossQuickBattle { get; set; }
        public bool AutoBossHighSpeedBattle { get; set; }
        public bool AutoBountyQuestDispatch { get; set; }
        public bool AutoBountyQuestReward { get; set; }

        public string DailyJobCron { get; set; } = "0 50 4 ? * *";
        public string HourlyJobCron { get; set; } = "0 30 0,4,8,12,16,20 ? * *";
        public string PvpJobCron { get; set; } = "0 0 20 ? * *";
        public string LegendLeagueJobCron { get; set; } = "0 10 20 ? * *";
        public string GuildRaidBossReleaseCron { get; set; } = "0 0 * ? * *";
        public string AutoBuyShopItemJobCron { get; set; } = "0 9 9,12,15,18 ? * *";
        public string AutoLocalRaidJobCron { get; set; } = "0 31 12,19 ? * *";
        public string AutoDeployGuildDefenseJobCron { get; set; } = "0 20 19 ? * *";
        public string AutoChangeGachaRelicJobCron { get; set; } = "0 40 4 ? * MON *";
        public string AutoDrawGachaRelicJobCron { get; set; } = "0 0 6 ? * SUN *";

        public List<string> DisabledDailyActions { get; set; } = new();
        public List<string> DisabledHourlyActions { get; set; } = new();
    }

    [ExportTsClass]
    public class ShopAutoBuyItem
    {
        public UserItem BuyItem { get; set; } = null!;
        public long ShopTabId { get; set; }
        public int MinDiscountPercent { get; set; }
        public UserItem ConsumeItem { get; set; } = null!;
    }

    [ExportTsClass]
    public class ShopConfig
    {
        public List<ShopAutoBuyItem> AutoBuyItems { get; set; } = new();
    }

    [ExportTsClass]
    public class DungeonBattleRelicSortInfo
    {
        public long Id { get; set; }
        public string Desc { get; set; } = string.Empty;
    }

    [ExportTsClass]
    public class GachaConfigModel
    {
        public List<UserItem> AutoGachaConsumeUserItems { get; set; } = new();
        public GachaRelicType TargetRelicType { get; set; } = GachaRelicType.None;
        public bool AutoGachaRelic { get; set; }
    }

    [ExportTsClass]
    public class DungeonBattleConfig
    {
        public List<ShopDiscountItem> ShopTargetItems { get; set; } = new();
        public bool PreferTreasureChest { get; set; }
        public int MaxUseRecoveryItem { get; set; }
        public bool AutoRemoveEquipment { get; set; }
    }

    [ExportTsClass]
    public class ShopDiscountItem : IUserItem
    {
        public int MinDiscountPercent { get; set; }
        public long ItemCount { get; set; }
        public long ItemId { get; set; }
        public ItemType ItemType { get; set; }
    }

    [ExportTsClass]
    public class ItemsConfig
    {
        public List<AutoUseItemType> AutoUseItemTypes { get; set; } = new();
    }

    [ExportTsClass]
    public class LocalRaidConfig
    {
        public List<WeightedItem> RewardItems { get; set; } = new();
        public bool SelfCreateRoom { get; set; }
        public int WaitSeconds { get; set; } = 3;

        /// <summary>Join mode: minutes to keep looking for a room before giving up.</summary>
        public int JoinWaitMinutes { get; set; } = 10;

        /// <summary>Join mode: when no room turns up in time, host one and run it solo (off = give up, like before 2.2).</summary>
        public bool HostWhenNoRoom { get; set; }

        /// <summary>Hosting: stop after this many losses in a row (losses don't use attempts). 0 = never stop.</summary>
        public int MaxSoloLosses { get; set; } = 3;

        /// <summary>Join mode: leave a joined room whose battle hasn't started after this many minutes, and look for another.</summary>
        public int RoomWaitMinutes { get; set; } = 3;

        /// <summary>Quest level to join/host (for low-level alts). 0 = pick automatically by reward weights.</summary>
        public int QuestLevel { get; set; }

        /// <summary>Try the next lower quest when no room shows up (join mode) or after MaxSoloLosses (hosting) instead of stopping.</summary>
        public bool DropToLowerQuest { get; set; }

        /// <summary>Join mode with DropToLowerQuest: minutes on one quest before trying a lower one.</summary>
        public int LowerQuestAfterMinutes { get; set; } = 3;

        /// <summary>Hard limit for the whole task, so no account stays stuck.</summary>
        public int MaxTaskMinutes { get; set; } = 20;
    }

    [ExportTsClass]
    public class WeightedItem : IUserItem
    {
        public double Weight { get; set; }
        public long ItemCount { get; set; }
        public long ItemId { get; set; }
        public ItemType ItemType { get; set; }
    }

    [ExportTsClass]
    public class BountyQuestAutoConfig
    {
        public List<UserItem> TargetItems { get; set; } = new();
    }
}

[ExportTsClass]
public class PlayerOption
{
    public long PlayerId { get; set; }
    public PvpOption BattleLeague { get; set; } = new();
    public PvpOption LegendLeague { get; set; } = new();
    public GameConfig.LocalRaidConfig LocalRaid { get; set; } = new();
    public FriendManageOption FriendManage { get; set; } = new();
    public GuildTowerOption GuildTower { get; set; } = new();
    public BountyQuestOption BountyQuest { get; set; } = new();
    public GameConfig.BountyQuestAutoConfig BountyQuestAuto { get; set; } = new();
    public GameConfig.GachaConfigModel GachaConfig { get; set; } = new();
    public Dictionary<QuickActionType, bool> QuickActionSwitch { get; set; } = new();
}

[ExportTsClass]
public class PvpOption
{
    public TargetSelectStrategy SelectStrategy { get; set; } = TargetSelectStrategy.Random;
    public List<CharacterFilter> CharacterFilters { get; set; } = new();
    public List<long> ExcludePlayerIds { get; set; } = new();
}

public enum TargetSelectStrategy
{
    Random,
    LowestBattlePower,
    HighestBattlePower
}

[ExportTsClass]
public class CharacterFilter
{
    public long CharacterId { get; set; }
    public CharacterFilterStrategy FilterStrategy { get; set; }
}

public enum CharacterFilterStrategy
{
    Character
}

[ExportTsClass]
public class BountyQuestOption
{
    public int MaxRefreshCount { get; set; }
    public Dictionary<string, int> TodayRefreshCount { get; set; } = new();
}

[ExportTsClass]
public class GuildTowerOption
{
    public bool AutoEntry { get; set; }
    public bool AutoChallenge { get; set; }
    public int AutoChallengeRetryCount { get; set; } = 10;
    public bool AutoReinforcement { get; set; }
    public bool AutoReceiveReward { get; set; }
}

[ExportTsClass]
public class FriendManageOption
{
    public bool AutoRemoveInactiveFriend { get; set; }
    public List<long> AutoRemoveWhitelist { get; set; } = new();
    public bool AutoSendFriendRequest { get; set; }
    public bool AutoAcceptFriendRequest { get; set; }
}

public enum QuickActionType
{
    DailyCheckIn,
    ReceiveVipGift,
    ReceiveMonthlyBoost,
    ReceiveAutoBattleReward,
    ReceiveFriendPoint,
    ReceiveInbox,
    ReinforcementEquipmentOnce,
    BossBattle,
    TowerBattle,
    BattleLeague,
    LegendLeague,
    BattleHighSpeed,
    FreeGacha,
    GuildCheckin,
    GuildRaid,
    GuildOpenBoss,
    ReceiveGvgReward,
    LocalRaid,
    BountyQuestReceive,
    BountyQuestDispatch,
    BuyShopItem,
    UseItem,
    RankUpCharacter,
    ReceiveAchievementReward,
}
