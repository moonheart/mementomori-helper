using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	[OrtegaApi("ranking/getPlayerRanking", true, false)]
	public class GetPlayerRankingRequest : ApiRequestBase
	{
	}
}
