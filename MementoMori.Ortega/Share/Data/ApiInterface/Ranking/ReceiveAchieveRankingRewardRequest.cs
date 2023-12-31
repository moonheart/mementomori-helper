using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	[OrtegaApi("ranking/receiveAchieveRankingReward", true, false)]
	public class ReceiveAchieveRankingRewardRequest : ApiRequestBase
	{
		public long AchieveRankingRewardMBId { get; set; }
	}
}
