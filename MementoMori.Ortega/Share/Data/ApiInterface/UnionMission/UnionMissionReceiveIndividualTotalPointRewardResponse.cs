using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission;

[MessagePackObject(true)]
public class UnionMissionReceiveIndividualTotalPointRewardResponse : ApiResponseBase, IUserSyncApiResponse
{
    public List<UserItem> RewardItemList { get; set; }

    public long ReceivedGuildPointRewardThreshold { get; set; }

    public long ReceivedPersonalPointRewardThreshold { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
