using MementoMori.Ortega.Share.Data.StarsGuidanceTradeShop;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.StarsGuidanceTradeShop
{
	[MessagePackObject(true)]
	[OrtegaApi("starsGuidanceTradeShop/getStarsGuidanceTradeShopInfo", true, false)]
	public class GetStarsGuidanceTradeShopInfoRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class GetStarsGuidanceTradeShopInfoResponse : ApiResponseBase
	{
		public long StarsGuidanceTradeShopId { get; set; }
		public long EndLocalTimestamp { get; set; }
		public ItemType GiveItemType { get; set; }
		public long GiveItemId { get; set; }
		public long TodayTradeCount { get; set; }
		public long LimitTradeCount { get; set; }
		public List<StarsGuidanceConsumeItemInfo> StarsGuidanceConsumeItemInfoList { get; set; }
	}
}
