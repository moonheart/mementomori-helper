using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/pvpStart", true, false)]
	public class PvpStartRequest : ApiRequestBase
	{
		public long RivalPlayerId
		{
			get;
			set;
		}

		public long RivalPlayerRank
		{
			get;
			set;
		}

		public PvpStartRequest()
		{
		}
	}
}
