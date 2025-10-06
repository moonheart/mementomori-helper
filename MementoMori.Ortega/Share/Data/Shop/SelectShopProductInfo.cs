using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	[Description("ショップ商品選択情報")]
	public class SelectShopProductInfo
	{
		public long MbId { get; set; }

        public string ProductId { get; set; }

        public ShopProductType ShopProductType { get; set; }

        public long ExpirationTime { get; set; }
	}
}
