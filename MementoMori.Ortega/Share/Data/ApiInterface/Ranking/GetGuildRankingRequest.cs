using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	[OrtegaApi("ranking/getGuildRanking", true, false)]
	public class GetGuildRankingRequest : ApiRequestBase
	{
	}
}
