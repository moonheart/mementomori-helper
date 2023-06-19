using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
	[MessagePackObject(true)]
	[OrtegaApi("loginBonus/receiveDailyLoginBonus", true, false)]
	public class ReceiveDailyLoginBonusRequest : ApiRequestBase
	{
		public int ReceiveDay
		{
			get;
			set;
		}

		public ReceiveDailyLoginBonusRequest()
		{
		}
	}
}
