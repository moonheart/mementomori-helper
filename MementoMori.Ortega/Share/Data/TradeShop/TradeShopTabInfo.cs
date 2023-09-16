using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TradeShop
{
	[MessagePackObject(true)]
	public class TradeShopTabInfo
	{
		[Description("交換所タブMBのID")]
		public long TradeShopTabId { get; set; }

		[Description("アイテムの種類")]
		public List<TradeShopItem> TradeShopItems { get; set; }

        [Description("最後に無料更新を行った時間")]
		public long LastFreeManualUpdateTime { get; set; }

		[Description("自動更新時間")]
		public long ExpirationTimeStamp { get; set; }
	}
}
