using System.Text.Json.Serialization;
using FreeSql.DataAnnotations;

namespace MementoMori.Api.Autopilot;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GrowthKind
{
    Level, LevelLink, Equip, Reinforce, Quest, Tower, RankUp, Gacha, Team, Friend, Reward, Info
}

/// <summary>
/// 成长记录: bot 每次真的让账号变强时记一条, 界面上用来证明它在干活
/// </summary>
[Index("idx_growth_user_time", "UserId,Time")]
public class GrowthEventEntity
{
    [Column(IsPrimary = true, IsIdentity = true)]
    public long Id { get; set; }

    public long UserId { get; set; }
    public DateTime Time { get; set; }

    [Column(MapType = typeof(string))]
    public GrowthKind Kind { get; set; }

    [Column(StringLength = -1)]
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// 每轮 autopilot 结束时的进度快照, 用来画进度曲线
/// </summary>
[Index("idx_snapshot_user_time", "UserId,Time")]
public class ProgressSnapshotEntity
{
    [Column(IsPrimary = true, IsIdentity = true)]
    public long Id { get; set; }

    public long UserId { get; set; }
    public DateTime Time { get; set; }
    public long QuestId { get; set; }
    public string QuestName { get; set; } = string.Empty;
    public long LinkLevel { get; set; }
    public long PlayerRank { get; set; }
    public long Gold { get; set; }
    public long Diamonds { get; set; }

    /// <summary>主线队伍战力</summary>
    public long Power { get; set; }

    /// <summary>TowerType -> 最高层, JSON</summary>
    [Column(StringLength = -1)]
    public string TowersJson { get; set; } = "{}";
}

/// <summary>
/// 用户在界面里改过的角色强度 (全局, 不分账号)
/// </summary>
public class TierOverrideEntity
{
    [Column(IsPrimary = true)]
    public long CharacterId { get; set; }

    public string Tier { get; set; } = string.Empty;
}

/// <summary>
/// Autopilot 配置: 默认全关, 用户自己挑要跑的步骤
/// </summary>
public class AutopilotConfig
{
    public bool Enabled { get; set; }
    public string Cron { get; set; } = "0 30 * ? * *";

    /// <summary>启用的步骤 (handler 的 ActionKey), 执行顺序由 AutopilotSteps 决定</summary>
    public List<string> Steps { get; set; } = new();

    /// <summary>抽卡后至少保留的免费钻石</summary>
    public long GachaDiamondReserve { get; set; } = 3000;

    /// <summary>值得用钻石抽的 UP 角色最低强度: 4 = S, 5 = SS</summary>
    public int GachaMinPickupTier { get; set; } = 4;

    /// <summary>
    /// 免费钻石超过这个数时, 多出来的部分也抽普通/非 S 卡池 (池子里也能出 S/SS). 0 = 关闭
    /// </summary>
    public long GachaSurplusAbove { get; set; }

    /// <summary>升星时允许吃同属性的低强度角色 (不只是重复卡)</summary>
    public bool RankUpWithFodder { get; set; } = true;
}
