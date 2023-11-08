using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class LegendLeagueStartResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public BattleResult BattleResult{ get; set; }

		public bool CanBattle { get; set; }

		public long GetPoint { get; set; }

		public long BeforePoint { get; set; }

		public long Ranking { get; set; }

		public long BeforeRanking { get; set; }

		public PlayerInfo RivalPlayerInfo{ get; set; }

		public UserSyncData UserSyncData{ get; set; }

		public long Point { get; set; }
	}
}
