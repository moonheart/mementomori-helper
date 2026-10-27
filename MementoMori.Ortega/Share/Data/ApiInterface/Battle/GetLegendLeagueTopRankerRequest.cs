using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Pvp;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/getLegendLeagueTopRanker", true, false)]
	public class GetLegendLeagueTopRankerRequest : ApiRequestBase
	{

	}

	[MessagePackObject(true)]
	public class GetLegendLeagueTopRankerResponse : ApiResponseBase
	{
		public List<LegendLeagueRankingPlayerInfo> TopRankerList { get; set; }
	}
}
