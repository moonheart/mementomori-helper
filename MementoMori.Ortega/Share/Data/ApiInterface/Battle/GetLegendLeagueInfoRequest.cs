using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/getLegendLeagueInfo", true, false)]
	public class GetLegendLeagueInfoRequest : ApiRequestBase
	{
		public bool IsRankingTab { get; set; }
	}
}
