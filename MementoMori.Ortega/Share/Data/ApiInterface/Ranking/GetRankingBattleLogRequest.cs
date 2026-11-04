using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	[OrtegaApi("ranking/getRankingBattleLog", true, false)]
	public class GetRankingBattleLogRequest : ApiRequestBase
	{
		public long TargetPlayerId { get; set; }

		public long AchieveRankingRewardId { get; set; }
	}
}
