using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori;

public class AuthOption
{
    [Obsolete]
    public string ClientKey { get; set; }
    public string DeviceToken { get; set; }
    public string AppVersion { get; set; }
    public string OSVersion { get; set; }
    public string ModelName { get; set; }
    [Obsolete]
    public long UserId { get; set; }
    public long LastLoginUserId { get; set; }

    public List<AccountInfo> Accounts { get; set; } = new();
}

public class AccountInfo
{
    public string Name { get; set; }
    public long UserId { get; set; }
    public string ClientKey { get; set; }
    public bool AutoLogin { get; set; }
}

public class GameConfig
{
    public class ShopDiscountItem : IUserItem
    {
        public long ItemCount { get; }
        public long ItemId { get; set; }
        public ItemType ItemType { get; set; }
        public int MinDiscountPercent { get; set; }
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
    }

    public class ShopConfig
    {
        public List<ShopAutoBuyItem> AutoBuyItems { get; set; } = new();

        public ShopConfig()
        {
        }
    }

    public class AutoJobModel
    {
        public bool DisableAll { get; set; }
        public bool AutoReinforcementEquipmentOneTime { get; set; }
        public bool AutoPvp { get; set; }
        public bool AutoDungeonBattle { get; set; }
        public bool AutoUseItems { get; set; }
        public bool AutoFreeGacha { get; set; }
        public bool AutoRankUpCharacter { get; set; }
        public bool AutoOpenGuildRaid { get; set; }

        public string DailyJobCron { get; set; } = "0 10 4 ? * *";
        public string HourlyJobCron { get; set; } = "0 30 0,4,8,12,16,20 ? * *";
        public string PvpJobCron { get; set; } = "0 0 20 ? * *";
        public string GuildRaidBossReleaseCron { get; set; } = "0 0 * ? * *";
        public string AutoBuyShopItemJobCron { get; set; } = "0 9 9,12,15,18 ? * *";
    }

    public class BountyQuestAutoModel
    {
        public List<UserItem> TargetItems { get; set; } = new();
        public int AllowedNonTargetItemCount { get; set; } = 100;
        public int AutoRefreshCount { get; set; }
    }

    public class DungeonBattleConfig
    {
        public List<ShopDiscountItem> ShopTargetItems { get; set; } = new();

        [Obsolete("Use ShopDiscountItem.MinDiscountPercent")]
        public int ShopDiscountPercent { get; set; } = 0;

        public bool PreferTreasureChest { get; set; }
        public int MaxUseRecoveryItem { get; set; }
    }

    public class LoginConfig
    {
        [Obsolete]
        public bool AutoLogin { get; set; }
    }

    public AutoJobModel AutoJob { get; set; } = new();
    public GachaConfigModel GachaConfig { get; set; } = new();
    public DungeonBattleRelicSortInfo[] DungeonBattleRelicSort { get; set; }
    public int AutoRequestDelay { get; set; }
    public bool RecordBattleLog { get; set; } = true;
    public string BattleLogDir { get; set; } = "BattleLogs/";
    public BountyQuestAutoModel BountyQuestAuto { get; set; } = new();
    public DungeonBattleConfig DungeonBattle { get; set; } = new();
    public ShopConfig Shop { get; set; } = new();
    public LoginConfig Login { get; set; } = new();
}