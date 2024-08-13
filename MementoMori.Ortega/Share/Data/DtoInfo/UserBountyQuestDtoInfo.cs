using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo;

[MessagePackObject(true)]
public class UserBountyQuestDtoInfo
{
    public int Date { get; set; }

    public long BountyQuestId { get; set; }

    public BountyQuestType BountyQuestType { get; set; }

    public long BountyQuestLimitStartTime { get; set; }

    public long BountyQuestEndTime { get; set; }

    public long RewardEndTime { get; set; }

    public bool IsReward { get; set; }

    public List<BountyQuestMemberInfo> StartMembers { get; set; }
}