using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
	[MessagePackObject(true)]
	[OrtegaApi("loginBonus/receiveLoginCountBonus", true, false)]
	public class ReceiveLoginCountBonusRequest : ApiRequestBase
	{
		public int ReceiveDayCount
		{
			get;
			set;
		}

		public ReceiveLoginCountBonusRequest()
		{
		}
	}
}
