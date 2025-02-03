using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TradeShop
{
	[MessagePackObject(true)]
	public class TradeShopItemInfo
	{
		[Description("TradeShopItemMBのId")]
		public long TradeShopItemId { get; set; }

        [Description("TradeShopDedicatedItemMBのID")]
        public long DedicatedItemId { get; set; }

		[Description("交換回数")]
		public int TradeCount { get; set; }
	}
}
