using MementoMori.Ortega.Share.Data.UnionMission;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission;

[MessagePackObject(true)]
public class UnionMissionGetInfoResponse : ApiResponseBase, IUserSyncApiResponse
{
    public UnionMissionClassType ClassType { get; set; }

    public UnionMissionClassType PrevClassType { get; set; }

    public bool ExistReceivableTotalPointReward { get; set; }

    public long GuildPoint { get; set; }

    public long GuildRank { get; set; }

    public int BlockGuildCount { get; set; }

    public List<UnionMissionPanelInfo> SharedMissionPanelList { get; set; }

    public List<UnionMissionPanelInfo> PersonalMissionPanelList { get; set; }

    public long PersonalAchievedCount { get; set; }

    public long PersonalPoint { get; set; }

    public long PersonalRank { get; set; }

    public long CumulativePersonalPoint { get; set; }

    public long ReceivedGuildPointRewardThreshold { get; set; }

    public long ReceivedPersonalPointRewardThreshold { get; set; }

    public int RemainingAcceptCount { get; set; }

    public int BuyAcceptCountToday { get; set; }

    public bool IsOpen { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
