using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	public class GetPlayerRankingResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public long AutoRanking { get; set; }

		public List<PlayerInfo> AutoRankingPlayerList { get; set; }

		public long AutoRankingToday { get; set; }

		public long BattlePower { get; set; }

		public long BattlePowerRanking { get; set; }

		public List<PlayerInfo> BattlePowerRankingPlayerList { get; set; }

		public long BattlePowerRankingToday { get; set; }

		public long PlayerRankRanking { get; set; }

		public List<PlayerInfo> PlayerRankRankingPlayerList { get; set; }

		public long PlayerRankRankingToday { get; set; }

		public long TowerBattleRanking { get; set; }

		public List<PlayerInfo> TowerBattleRankingPlayerList { get; set; }

		public long TowerBattleRankingToday { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
