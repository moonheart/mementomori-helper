using MessagePack;

namespace MementoMori.Ortega.Share.Data.WebStore
{
	[MessagePackObject(true)]
	public class WebStoreSdkInfo
	{
		public long WebStoreProductId { get; set; }

		public long BuyPrice { get; set; }
	}
}
