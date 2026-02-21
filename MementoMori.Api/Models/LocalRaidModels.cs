using MementoMori.Ortega.Share.Data.LocalRaid;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

/// <summary>
/// LocalRaid 会话状态
/// </summary>
[ExportTsClass]
public class LocalRaidSessionState
{
    /// <summary>
    /// 是否已连接到 MagicOnion
    /// </summary>
    public bool IsConnected { get; set; }
    
    /// <summary>
    /// 是否在房间中
    /// </summary>
    public bool IsInRoom { get; set; }
    
    /// <summary>
    /// 当前房间信息
    /// </summary>
    public LocalRaidPartyInfo? CurrentRoom { get; set; }
}

/// <summary>
/// LocalRaid 错误信息
/// </summary>
[ExportTsClass]
public class LocalRaidError
{
    /// <summary>
    /// 错误码
    /// </summary>
    public int ErrorCode { get; set; }
    
    /// <summary>
    /// 错误消息
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

/// <summary>
/// 会话信息
/// </summary>
[ExportTsClass]
public class LocalRaidSessionInfo
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// 玩家ID
    /// </summary>
    public long PlayerId { get; set; }
    
    /// <summary>
    /// 玩家名称
    /// </summary>
    public string PlayerName { get; set; } = string.Empty;
    
    /// <summary>
    /// 是否已连接
    /// </summary>
    public bool IsConnected { get; set; }
}
