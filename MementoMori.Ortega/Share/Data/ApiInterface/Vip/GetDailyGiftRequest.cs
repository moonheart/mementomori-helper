using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Vip
{
	[OrtegaApi("vip/getDailyGift", true, false)]
	[MessagePackObject(true)]
	public class GetDailyGiftRequest : ApiRequestBase
	{
		public GetDailyGiftRequest()
		{
		}
	}
}
