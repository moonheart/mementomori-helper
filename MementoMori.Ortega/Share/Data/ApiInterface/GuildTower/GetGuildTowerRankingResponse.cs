using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Guild;
using MementoMori.Ortega.Share.Data.GuildTower;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	public class GetGuildTowerRankingResponse : ApiResponseBase
	{
		public long CurrentGuildRanking { get; set; }

		public long CurrentTotalStarRanking { get; set; }

		public long MaxClearedFloorMBId { get; set; }

		public long TodayGuildRanking { get; set; }

		public long TodayTotalStarRanking { get; set; }

		public List<GuildRank> TopGuildRankList { get; set; }

		public List<GuildTowerTotalStarRankingInfo> TopTotalStarRankingPlayerList { get; set; }

		public long TotalStarCount { get; set; }

		public Dictionary<JobFlags, List<GuildTowerReinforcementJobRankingData>> ReinforcementJobRankingDataListMap { get; set; }
	}
}
