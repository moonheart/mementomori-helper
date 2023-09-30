using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori;

public class AuthOption
{
    public string ClientKey { get; set; }
    public string DeviceToken { get; set; }
    public string AppVersion { get; set; }
    public string OSVersion { get; set; }
    public string ModelName { get; set; }
    public long UserId { get; set; }
}

public class GameConfig
{
    public class DungeonBattleRelicSortInfo
    {
        public long Id { get; set; }
        public string Desc { get; set; }
    }

    public class GachaConfigModel
    {
        public List<UserItem> AutoGachaConsumeUserItems { get; set; } = new();
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
    }

    public class BountyQuestAutoModel
    {
        public List<UserItem> TargetItems { get; set; } = new();
        public int AllowedNonTargetItemCount { get; set; } = 100;
        public int AutoRefreshCount { get; set; }
    }

    public class DungeonBattleConfig
    {
        public List<UserItem> ShopTargetItems { get; set; } = new();
    }

    public AutoJobModel AutoJob { get; set; } = new();
    public GachaConfigModel GachaConfig { get; set; } = new();
    public DungeonBattleRelicSortInfo[] DungeonBattleRelicSort { get; set; }
    public int AutoRequestDelay { get; set; }
    public BountyQuestAutoModel BountyQuestAuto { get; set; } = new();
    public DungeonBattleConfig DungeonBattle { get; set; } = new();
}