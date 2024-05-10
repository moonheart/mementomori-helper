using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	[Description("Stripe商品情報")]
	public class StripeShopProductInfo
	{
		public long GivePlayerId { get; set; }

		public long MbId { get; set; }

		public string ProductId { get; set; }

		public ShopProductType ShopProductType { get; set; }

		public string SessionId { get; set; }

		public bool IsStripePaidStatus { get; set; }
	}
}
