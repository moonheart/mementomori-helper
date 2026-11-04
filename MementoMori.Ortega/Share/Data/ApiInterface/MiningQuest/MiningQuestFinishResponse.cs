using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.MiningQuest;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestFinishResponse : ApiResponseBase, IUserSyncApiResponse
{
    public List<MiningQuestMinedBlockInfo> MinedBlockInfoList { get; set; }

    public long Score { get; set; }

    public MiningQuestGradeType GradeType { get; set; }

    public string GradeNameTextKey { get; set; }

    public UserItem RewardItem { get; set; }

    public long TotalRewardCount { get; set; }

    public long MaxRewardCount { get; set; }

    public bool IsDisplayMissionBadge { get; set; }

    public bool IsDisplayGradeBadge { get; set; }

    public bool IsDisplayMaxRewardCountBadge { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
