using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Pvp;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/getLegendLeagueResultDetail", true, false)]
	public class GetLegendLeagueResultDetailRequest : ApiRequestBase
	{
		public string BattleToken { get; set; }
	}

	[MessagePackObject(true)]
	public class GetLegendLeagueResultDetailResponse : ApiResponseBase
	{
		public BattleResult BattleResult { get; set; }
	}
}
