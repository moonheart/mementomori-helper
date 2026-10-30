using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Guild;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	public class GetGuildRankingResponse : ApiResponseBase
	{
		public long GuildApplyingCount { get; set; }

		public long GuildBattlePowerMax { get; set; }

		public long GuildBattlePowerRanking { get; set; }

		public List<GuildRank> GuildBattlePowerRankingGuildList { get; set; }

		public long GuildLvRanking { get; set; }

		public List<GuildRank> GuildLvRankingGuildList { get; set; }

		public long GuildLvRankingToday { get; set; }

		public long GuildStockRanking { get; set; }

		public List<GuildRank> GuildStockRankingGuildList { get; set; }

		public long GuildStockTodayCount { get; set; }
	}
}
