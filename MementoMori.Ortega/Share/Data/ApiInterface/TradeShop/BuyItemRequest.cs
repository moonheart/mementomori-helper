using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.TradeShop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TradeShop
{
	[MessagePackObject(true)]
	[OrtegaApi("tradeShop/buyItem", true, false)]
	public class BuyItemRequest : ApiRequestBase
	{
		public long TradeShopTabId { get; set; }

		public List<TradeShopItemInfo> TradeShopItemInfos { get; set; }

        public long TradeShopSphereId { get; set; }

		public int TradeSphereCount { get; set; }
	}
}
