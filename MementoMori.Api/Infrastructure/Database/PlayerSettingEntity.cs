using FreeSql.DataAnnotations;

namespace MementoMori.Api.Infrastructure.Database;

/// <summary>
/// 玩家设置实体 - 用于存储各种配置子项（如 AutoJob, Shop 等）
/// </summary>
public class PlayerSettingEntity
{
    /// <summary>
    /// 玩家ID
    /// </summary>
    [Column(IsPrimary = true)]
    public long UserId { get; set; }

    /// <summary>
    /// 配置子项键名 (例如 'AutoJob', 'Shop')
    /// </summary>
    [Column(IsPrimary = true)]
    public string SettingKey { get; set; } = string.Empty;

    /// <summary>
    /// JSON 序列化的配置内容
    /// </summary>
    [Column(StringLength = -1)] // TEXT 类型，不限制长度
    public string JsonValue { get; set; } = string.Empty;

    /// <summary>
    /// 最后更新时间
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
