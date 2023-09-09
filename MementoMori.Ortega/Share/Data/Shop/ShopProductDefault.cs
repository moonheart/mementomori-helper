using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductDefault
	{
		[Description("お得バッジ画像ID")]
		public long BadgeImageId { get; set; }

		[Description("告知用大画像ID")]
		public long BigImageId { get; set; }

		[Description("期間内購入回数")]
		public int BuyCountPeriod { get; set; }

		[Description("購入回数制限の制限回数")]
		public int BuyLimitCount { get; set; }

		[Description("ディスカウント率")]
		public int DiscountRate { get; set; }

		[Description("終了日時")]
		public string EndDateTime { get; set; }

        [Description("画像ID")]
		public long ImageId { get; set; }

		[Description("無料フラグ")]
		public bool IsFree { get; set; }

		[Description("ShopProductDefaultMBのId")]
		public long MBId { get; set; }

		[Description("商品名キー")]
		public string NameKey { get; set; }

        [Description("ProductId")]
		public string ProductId { get; set; }

        [Description("購入回数リセット時間")]
		public long ResetTime { get; set; }

		[Description("購入回数制限タイプ")]
		public ShopBuyLimitType ShopBuyLimitType { get; set; }

		[Description("商品値段")]
		public int ShopProductPrice { get; set; }

		[Description("商品UIタイプ")]
		public ShopProductUiType ShopProductUiType { get; set; }

		[Description("報酬リスト")]
		public List<ShopItem> UserItemList { get; set; }
    }
}
