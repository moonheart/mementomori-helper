using FreeSql.DataAnnotations;

namespace MementoMori.Api.Infrastructure.Database;

/// <summary>
/// 玩家账号实体 - 持久化存储
/// </summary>
public class AccountEntity
{
    /// <summary>
    /// 玩家唯一ID
    /// </summary>
    [Column(IsPrimary = true, IsIdentity = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 客户端密钥
    /// </summary>
    public string ClientKey { get; set; } = string.Empty;

    /// <summary>
    /// 玩家备注名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 最后登录的世界ID
    /// </summary>
    public long? CurrentWorldId { get; set; }

    /// <summary>
    /// Ortega AccessToken（用于重启后复用会话）
    /// </summary>
    public string OrtegaAccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 上次会话的 Game API Host（用于重启后直接恢复）
    /// </summary>
    public string GameApiHost { get; set; } = string.Empty;

    /// <summary>
    /// Ortega 设备唯一标识（用于避免异地设备会话冲突）
    /// </summary>
    public string OrtegaUuid { get; set; } = string.Empty;
}
