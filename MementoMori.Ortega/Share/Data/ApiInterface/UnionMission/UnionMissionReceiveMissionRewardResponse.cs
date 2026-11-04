using MementoMori.Ortega.Share.Data.UnionMission;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission;

[MessagePackObject(true)]
public class UnionMissionReceiveMissionRewardResponse : ApiResponseBase, IUserSyncApiResponse
{
    public long GainedPoint { get; set; }

    public long GuildPoint { get; set; }

    public long GuildRank { get; set; }

    public List<UnionMissionPanelInfo> SharedMissionPanelList { get; set; }

    public List<UnionMissionPanelInfo> PersonalMissionPanelList { get; set; }

    public long PersonalAchievedCount { get; set; }

    public long PersonalPoint { get; set; }

    public long PersonalRank { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
