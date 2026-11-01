using MementoMori.Ortega.Share.Data.MiningQuest;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestGetInfoResponse : ApiResponseBase
{
    public long MiningQuestEventId { get; set; }

    public long MaxScore { get; set; }

    public MiningQuestGradeType GradeType { get; set; }

    public string GradeNameTextKey { get; set; }

    public long EndLocalTimeStamp { get; set; }

    public bool IsDisplayMissionBadge { get; set; }

    public bool IsDisplayGradeBadge { get; set; }

    public bool IsDisplayBenefitBadge { get; set; }

    public bool IsDisplayMaxRewardCountBadge { get; set; }

    public int CurrentAttackPower { get; set; }

    public int CurrentAttackFrequency { get; set; }

    public int CurrentCollectionLevel { get; set; }

    public List<UserMiningQuestReinforcementInfo> UserReinforcementInfoList { get; set; }
}
