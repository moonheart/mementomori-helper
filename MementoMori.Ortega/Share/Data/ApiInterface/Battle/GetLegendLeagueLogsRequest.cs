using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.Pvp;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/getLegendLeagueLogs", true, false)]
	public class GetLegendLeagueLogsRequest : ApiRequestBase
	{

	}

	[MessagePackObject(true)]
	public class GetLegendLeagueLogsResponse : ApiResponseBase
	{
		public List<LegendLeagueLogInfo> AttackLegendLeagueLogInfoList { get; set; }
		public List<LegendLeagueLogInfo> DefenseLegendLeagueLogInfoList { get; set; }
	}
}
