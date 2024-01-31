using MementoMori.Ortega.Share.Data.Battle;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle;

[MessagePackObject(true)]
public class GetPvpInfoResponse : ApiResponseBase, IUserSyncApiResponse
{
    public long CurrentRank { get; set; }

    public bool ExistNewDefenseBattleLog { get; set; }

    public List<PvpRankingPlayerInfo> MatchingRivalList { get; set; }

    public List<PvpRankingPlayerInfo> TopRankerList { get; set; }

    public UserSyncData UserSyncData { get; set; }
}