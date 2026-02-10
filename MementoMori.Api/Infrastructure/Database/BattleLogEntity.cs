using FreeSql.DataAnnotations;

namespace MementoMori.Api.Infrastructure.Database;

/// <summary>
/// 战斗日志实体 - 存储战斗结果
/// </summary>
public class BattleLogEntity
{
    /// <summary>
    /// 战斗唯一标识 (BattleToken)
    /// </summary>
    [Column(IsPrimary = true, IsIdentity = false)]
    public string BattleToken { get; set; } = string.Empty;

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 记录创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// 战斗类型 (1=Auto, 2=Boss, 3=GuildBattle, 5=BattleLeague, 6=LegendLeague, 7=LocalRaid, 8=TowerBattle, 9=DungeonBattle, 11=GuildRaid, 12=GuildTower)
    /// </summary>
    public int BattleType { get; set; }

    /// <summary>
    /// 关卡ID (可选)
    /// </summary>
    public long? QuestId { get; set; }

    /// <summary>
    /// 是否胜利
    /// </summary>
    public bool IsWin { get; set; }

    /// <summary>
    /// 结束回合数
    /// </summary>
    public int EndTurn { get; set; }

    /// <summary>
    /// 战斗时长 (毫秒)
    /// </summary>
    public long BattleDurationMs { get; set; }

    /// <summary>
    /// API 来源路径
    /// </summary>
    public string ApiUri { get; set; } = string.Empty;

    /// <summary>
    /// 完整战斗数据 (JSON 格式)
    /// </summary>
    [Column(StringLength = -1)] // TEXT 类型
    public string BattleDataJson { get; set; } = string.Empty;
}
