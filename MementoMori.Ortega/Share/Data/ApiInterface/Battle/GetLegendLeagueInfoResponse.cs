using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class GetLegendLeagueInfoResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public int ConsecutiveVictoryCount { get; set; }

		public long CurrentPoint { get; set; }

		public long CurrentRanking { get; set; }

		public bool ExistNewDefenseBattleLog { get; set; }

        public bool IsDisplayRewardBadge { get; set; }

		public bool IsInTimeCanChallenge { get; set; }

		public List<LegendLeagueRankingPlayerInfo> MatchingRivalList{ get; set; }

		public long PvpBattlePower { get; set; }

		public List<LegendLeagueRankingPlayerInfo> TopRankerList{ get; set; }

		public UserSyncData UserSyncData{ get; set; }
	}
}
