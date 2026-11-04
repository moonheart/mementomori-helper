using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Models.DTOs;

/// <summary>
/// 计算/派遣悬赏任务请求
/// </summary>
public class CalculateBountyQuestFormationRequest
{
    /// <summary>
    /// 悬赏任务ID（单体操作）
    /// </summary>
    public long? BountyQuestId { get; set; }

    /// <summary>
    /// 悬赏任务类型（批量操作）
    /// </summary>
    public BountyQuestType? BountyQuestType { get; set; }
}
