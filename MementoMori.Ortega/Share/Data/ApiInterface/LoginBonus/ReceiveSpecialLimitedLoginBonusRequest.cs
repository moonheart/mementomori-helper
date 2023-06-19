using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
	[MessagePackObject(true)]
	[OrtegaApi("loginBonus/receiveSpecialLimitedLoginBonus", true, false)]
	public class ReceiveSpecialLimitedLoginBonusRequest : ApiRequestBase
	{
		public long LimitedLoginBonusId
		{
			get;
			set;
		}

		public ReceiveSpecialLimitedLoginBonusRequest()
		{
		}
	}
}
