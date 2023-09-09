using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductGrowthPackDetail
	{
		[Description("バフ情報")]
		public ShopGrowthPackBuffInfo BuffInfo { get; set; }

        [Description("現在購入回数")]
		public int CurrentBuyCount { get; set; }

		[Description("バフか否か")]
		public bool IsBuff { get; set; }

		[Description("無料フラグ")]
		public bool IsFree { get; set; }

		[Description("購入可能回数")]
		public int MaxBuyCount { get; set; }

		[Description("MBのID")]
		public long MBId { get; set; }

		[Description("ProductId")]
		public string ProductId { get; set; }

        [Description("商品値段")]
		public int ShopProductPrice { get; set; }

		[Description("報酬リスト")]
		public List<ShopItem> ShopItemList { get; set; }
    }
}
