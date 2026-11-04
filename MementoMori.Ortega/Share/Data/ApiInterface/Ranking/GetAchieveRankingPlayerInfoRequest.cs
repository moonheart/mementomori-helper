using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	[OrtegaApi("ranking/getAchieveRankingPlayerInfo", true, false)]
	public class GetAchieveRankingPlayerInfoRequest : ApiRequestBase
	{
		public long AchieveRankingRewardMBId { get; set; }
	}
}
