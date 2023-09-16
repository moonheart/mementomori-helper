using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TradeShop
{
	[MessagePackObject(true)]
	[OrtegaApi("tradeShop/resetTab", true, false)]
	public class ResetTabRequest : ApiRequestBase
	{
		public long TradeShopTabId { get; set; }
	}
}
