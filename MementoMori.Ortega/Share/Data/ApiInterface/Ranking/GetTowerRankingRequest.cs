using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	[OrtegaApi("ranking/getTowerRanking", true, false)]
	public class GetTowerRankingRequest : ApiRequestBase
	{
	}
}
