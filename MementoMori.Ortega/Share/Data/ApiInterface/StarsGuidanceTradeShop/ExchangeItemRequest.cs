using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.StarsGuidanceTradeShop
{
	[MessagePackObject(true)]
	[OrtegaApi("starsGuidanceTradeShop/exchangeItem", true, false)]
	public class ExchangeItemRequest : ApiRequestBase
	{
		public long StarsGuidanceTradeShopId { get; set; }
		public UserItem ConsumeItem { get; set; }
	}

	[MessagePackObject(true)]
	public class ExchangeItemResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserItem GiveItem { get; set; }
		public long TodayTradeCount { get; set; }
		public UserSyncData UserSyncData { get; set; }
	}
}
