using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	[OrtegaApi("ranking/getBulkAchieveRankingPlayerInfo", true, false)]
	public class GetBulkAchieveRankingPlayerInfoRequest : ApiRequestBase
	{
		public List<long> AchieveRankingRewardMBIdList { get; set; }
	}
}
