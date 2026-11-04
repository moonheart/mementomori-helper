using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Ranking;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	public class GetBulkAchieveRankingPlayerInfoResponse : ApiResponseBase
	{
		public Dictionary<long, AchieveRankingPlayerInfo> AchieveRankingPlayerInfoMap { get; set; }
	}
}
