using MementoMori.Api.Models;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Import;

/// <summary>
/// 旧 Blazor 版 appsettings.user.json / appsettings.json 的结构。
/// 只用于一次性导入, 不参与新配置的运行时读取。
/// </summary>
public class LegacyRoot
{
    public LegacyAuthOption AuthOption { get; set; } = new();
    public LegacyGameConfig GameConfig { get; set; } = new();
    public Dictionary<string, LegacyPlayerOption> PlayersOption { get; set; } = new();
}

public class LegacyAuthOption
{
    public string? ClientKey { get; set; }
    public string? AuthUrl { get; set; }
    public string? DeviceToken { get; set; }
    public string? AppVersion { get; set; }
    public string? OSVersion { get; set; }
    public string? ModelName { get; set; }
    public long UserId { get; set; }
    public long LastLoginUserId { get; set; }
    public List<LegacyAccountInfo>? Accounts { get; set; }
}

public class LegacyAccountInfo
{
    public string? Name { get; set; }
    public long UserId { get; set; }
    public string? ClientKey { get; set; }
    public bool AutoLogin { get; set; }
    public long AutoLoginWorldId { get; set; }
}

public class LegacyGameConfig
{
    public LegacyAutoJob? AutoJob { get; set; }
    public GameConfig.GachaConfigModel? GachaConfig { get; set; }
    public List<GameConfig.DungeonBattleRelicSortInfo>? DungeonBattleRelicSort { get; set; }
    public int? AutoRequestDelay { get; set; }
    public bool? RecordBattleLog { get; set; }
    public bool? ReportBattleLog { get; set; }
    public string? BattleLogDir { get; set; }
    public GameConfig.BountyQuestAutoConfig? BountyQuestAuto { get; set; }
    public LegacyDungeonBattle? DungeonBattle { get; set; }
    public GameConfig.ShopConfig? Shop { get; set; }
    public GameConfig.LocalRaidConfig? LocalRaid { get; set; }
    public string? ServerUrl { get; set; }
    public LegacyLogin? Login { get; set; }
    public GameConfig.ItemsConfig? Items { get; set; }
}

/// <summary>
/// 旧 AutoJob 多一个 AutoReinforcementEquipmentOneTime 字段。
/// </summary>
public class LegacyAutoJob : GameConfig.AutoJobModel
{
    public bool? AutoReinforcementEquipmentOneTime { get; set; }
}

/// <summary>
/// 旧 DungeonBattle 多一个已废弃的 ShopDiscountPercent。
/// </summary>
public class LegacyDungeonBattle : GameConfig.DungeonBattleConfig
{
    public int? ShopDiscountPercent { get; set; }
}

public class LegacyLogin
{
    public bool? AutoLogin { get; set; }
}

public class LegacyPlayerOption
{
    public long PlayerId { get; set; }
    public LegacyPvpOption? BattleLeague { get; set; }
    public LegacyPvpOption? LegendLeague { get; set; }
    public GameConfig.LocalRaidConfig? LocalRaid { get; set; }
    public FriendManageOption? FriendManage { get; set; }
    public GuildTowerOption? GuildTower { get; set; }
    public BountyQuestOption? BountyQuest { get; set; }
    public GameConfig.BountyQuestAutoConfig? BountyQuestAuto { get; set; }
    public GameConfig.GachaConfigModel? GachaConfig { get; set; }
    public Dictionary<string, bool>? QuickActionSwitch { get; set; }
}

public class LegacyPvpOption
{
    public TargetSelectStrategy SelectStrategy { get; set; }
    public List<LegacyCharacterFilter>? CharacterFilters { get; set; }
    public List<long>? ExcludePlayerIds { get; set; }
}

public class LegacyCharacterFilter
{
    public long CharacterId { get; set; }
    public LegacyCharacterFilterStrategy FilterStrategy { get; set; }
    public BattleParameterType BattleParameterType { get; set; }
}

public enum LegacyCharacterFilterStrategy
{
    Character,
    PropertyMoreThanSelf
}
