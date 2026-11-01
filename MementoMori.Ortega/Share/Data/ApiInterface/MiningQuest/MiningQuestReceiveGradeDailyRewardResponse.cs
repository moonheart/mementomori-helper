using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestReceiveGradeDailyRewardResponse : ApiResponseBase, IUserSyncApiResponse
{
    public List<UserItem> RewardItems { get; set; }

    public bool IsDisplayMissionBadge { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
