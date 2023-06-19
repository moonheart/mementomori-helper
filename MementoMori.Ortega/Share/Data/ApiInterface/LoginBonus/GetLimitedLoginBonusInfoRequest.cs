using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
	[OrtegaApi("loginBonus/getLimitedLoginBonusInfo", true, false)]
	[MessagePackObject(true)]
	public class GetLimitedLoginBonusInfoRequest : ApiRequestBase
	{
		public long LimitedLoginBonusId
		{
			get;
			set;
		}

		public GetLimitedLoginBonusInfoRequest()
		{
		}
	}
}
