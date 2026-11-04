using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Pvp;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/getPvpTopRanker", true, false)]
	public class GetPvpTopRankerRequest : ApiRequestBase
	{

	}

	[MessagePackObject(true)]
	public class GetPvpTopRankerResponse : ApiResponseBase
	{
		public List<PvpRankingPlayerInfo> TopRankerList { get; set; }
	}
}
