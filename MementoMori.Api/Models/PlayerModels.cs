using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

/// <summary>
/// 玩家世界信息
/// </summary>
[ExportTsClass]
public class PlayerWorldDto
{
    public long WorldId { get; set; }
    public long PlayerId { get; set; }
    public string WorldName { get; set; } = string.Empty;
    public long LastLoginTime { get; set; }
}

/// <summary>
/// 玩家信息
/// </summary>
[ExportTsClass]
public class PlayerInfoDto
{
    public long PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int Level { get; set; }
    public long WorldId { get; set; }
}
