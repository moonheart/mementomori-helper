using TypeGen.Core.TypeAnnotations;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Data.Mission;

namespace MementoMori.Api.Models;

/// <summary>
/// 获取任务信息请求
/// </summary>
[ExportTsClass]
public class ApiGetMissionInfoRequest
{
    public List<MissionGroupType> MissionGroupTypes { get; set; } = new();
}

/// <summary>
/// 获取任务信息响应
/// </summary>
[ExportTsClass]
public class ApiGetMissionInfoResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Dictionary<MissionGroupType, MissionInfo>? MissionInfoDict { get; set; }
}

/// <summary>
/// 领取任务奖励请求
/// </summary>
[ExportTsClass]
public class ApiClaimMissionRequest
{
    public List<long> MissionIds { get; set; } = new();
}

/// <summary>
/// 领取任务奖励响应
/// </summary>
[ExportTsClass]
public class ApiClaimMissionResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public AcquisitionMissionRewardInfo? RewardInfo { get; set; }
}

/// <summary>
/// 领取功勋/活跃度奖励请求
/// </summary>
[ExportTsClass]
public class ApiClaimActivityRewardRequest
{
    public MissionGroupType MissionGroupType { get; set; }
    public long RequiredCount { get; set; }
}

/// <summary>
/// 领取功勋/活跃度奖励响应
/// </summary>
[ExportTsClass]
public class ApiClaimActivityRewardResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public AcquisitionMissionRewardInfo? RewardInfo { get; set; }
}
