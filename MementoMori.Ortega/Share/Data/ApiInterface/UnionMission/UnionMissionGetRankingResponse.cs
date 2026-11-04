using MementoMori.Ortega.Share.Data.UnionMission;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission;

[MessagePackObject(true)]
public class UnionMissionGetRankingResponse : ApiResponseBase
{
    public long GuildRank { get; set; }

    public long TodayGuildRank { get; set; }

    public long GuildPoint { get; set; }

    public List<UnionMissionGuildRankingEntry> GuildRankingList { get; set; }

    public long PersonalRank { get; set; }

    public long TodayPersonalRank { get; set; }

    public long PersonalPoint { get; set; }

    public int PersonalAcceptedCount { get; set; }

    public int PersonalAchievedCount { get; set; }

    public List<UnionMissionPersonalRankingEntry> PersonalRankingList { get; set; }
}
