using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
	[OrtegaApi("loginBonus/getMonthlyLoginBonusInfo", true, false)]
	[MessagePackObject(true)]
	public class GetMonthlyLoginBonusInfoRequest : ApiRequestBase
	{
		public GetMonthlyLoginBonusInfoRequest()
		{
		}
	}
}
