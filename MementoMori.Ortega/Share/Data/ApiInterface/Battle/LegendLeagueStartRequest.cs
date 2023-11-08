using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/legendLeagueStart", true, false)]
	public class LegendLeagueStartRequest : ApiRequestBase
	{
		public long RivalPlayerId { get; set; }
	}
}
