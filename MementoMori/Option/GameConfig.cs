using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori;

public class GameConfig
{
    public enum AutoUseItemType
    {
        Others,

        /// <summary>
        ///     一袋钻石
        /// </summary>
        DiamondBag,

        /// <summary>
        ///     未鉴定符石
        /// </summary>
        MysteryRune,

        /// <summary>
        ///     魔女的来信 R
        /// </summary>
        WitchLetterR,

        /// <summary>
        ///     魔女的来信 SR
        /// </summary>
        WitchLetterSr,

        /// <summary>
        ///     魔女的心片
        /// </summary>
        WitchShard,

        /// <summary>
        ///     小壶
        /// </summary>
        Pot,

        /// <summary>
        ///     大壶
        /// </summary>
        Amphora,

        /// <summary>
        ///     封印宝箱
        /// </summary>
        SealedChest,

        /// <summary>
        ///     魔女的来信礼盒
        /// </summary>
        WitchLetterGift,

        /// <summary>
        ///     命运盒
        /// </summary>
        ChestOfChance
    }


    public AutoJobModel AutoJob { get; set; } = new();

    [Obsolete("Use config in PlayerOption")]
    public GachaConfigModel GachaConfig { get; set; } = new();

    public DungeonBattleRelicSortInfo[] DungeonBattleRelicSort { get; set; }
    public int AutoRequestDelay { get; set; }
    public bool RecordBattleLog { get; set; } = true;
    public bool ReportBattleLog { get; set; } = true;
    public string BattleLogDir { get; set; } = "BattleLogs/";
    public BountyQuestAutoModel BountyQuestAuto { get; set; } = new();
    public DungeonBattleConfig DungeonBattle { get; set; } = new();
    public ShopConfig Shop { get; set; } = new();

    [Obsolete("Use config in PlayerOption")]
    public LocalRaidConfig LocalRaid { get; set; } = new();

    public string ServerUrl { get; set; }

    public LoginConfig Login { get; set; } = new();
    public ItemsConfig Items { get; set; } = new();

    public class ShopDiscountItem : IUserItem
    {
        public int MinDiscountPercent { get; set; }
        public long ItemCount { get; }
        public long ItemId { get; set; }
        public ItemType ItemType { get; set; }
    }

    public class ShopAutoBuyItem
    {
        public UserItem BuyItem { get; set; }
        public long ShopTabId { get; set; }
        public int MinDiscountPercent { get; set; }
        public UserItem ConsumeItem { get; set; }
    }

    public class DungeonBattleRelicSortInfo
    {
        public long Id { get; set; }
        public string Desc { get; set; }
    }

    public class GachaConfigModel
    {
        public List<UserItem> AutoGachaConsumeUserItems { get; set; } = new();

        public GachaRelicType TargetRelicType { get; set; } = GachaRelicType.None;

        public bool AutoGachaRelic { get; set; }
    }

    public class ShopConfig
    {
        public List<ShopAutoBuyItem> AutoBuyItems { get; set; } = new();
    }

    public class AutoJobModel
    {
        public bool DisableAll { get; set; }
        public bool AutoReinforcementEquipmentOneTime { get; set; }
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
    }

    public class BountyQuestAutoModel
    {
        public List<UserItem> TargetItems { get; set; } = new();
    }

    public class WeightedItem : IUserItem
    {
        public WeightedItem(ItemType itemType, long itemId, double weight)
        {
            ItemId = itemId;
            ItemType = itemType;
            Weight = weight;
        }

        public double Weight { get; set; }

        public long ItemCount { get; set; }
        public long ItemId { get; set; }
        public ItemType ItemType { get; set; }
    }

    public class LocalRaidConfig
    {
        public List<WeightedItem> RewardItems { get; set; } = new();
        // {
        //     new WeightedItem(ItemType.ExchangePlaceItem, 4, 4), // 符石兑换券
        //     new WeightedItem(ItemType.CharacterTrainingMaterial, 2, 3), // 潜能宝珠
        //     new WeightedItem(ItemType.EquipmentReinforcementItem, 2, 2.5), // 强化秘药
        //     new WeightedItem(ItemType.CharacterTrainingMaterial, 1, 2), // 经验珠
        //     new WeightedItem(ItemType.EquipmentReinforcementItem, 1, 1), // 强化水
        // };

        public bool SelfCreateRoom { get; set; }

        public int WaitSeconds { get; set; } = 3;
    }

    public class DungeonBattleConfig
    {
        public List<ShopDiscountItem> ShopTargetItems { get; set; } = new();

        [Obsolete("Use ShopDiscountItem.MinDiscountPercent")]
        public int ShopDiscountPercent { get; set; } = 0;

        public bool PreferTreasureChest { get; set; }
        public int MaxUseRecoveryItem { get; set; }

        public bool AutoRemoveEquipment { get; set; }
    }

    [Obsolete]
    public class LoginConfig
    {
        [Obsolete]
        public bool AutoLogin { get; set; }
    }

    public class ItemsConfig
    {
        public List<AutoUseItemType> AutoUseItemTypes { get; set; } = new();
    }
}