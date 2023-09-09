using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductMonthlyBoost
	{
		[Description("お得バッジ画像ID")]
		public long BadgeImageId { get; set; }

		[Description("割引率")]
		public int DiscountRate { get; set; }

		[Description("期限")]
		public long ExpirationTimeStamp { get; set; }

		[Description("ヘルプパス")]
		public string HelpPath { get; set; }

        [Description("デイリー報酬を受取済みか")]
		public bool IsAlreadyReceive { get; set; }

		[Description("事前購入フラグ")]
		public bool IsPrePurchased { get; set; }

		[Description("商品名キー")]
		public string NameKey { get; set; }

        [Description("パネル画像ID")]
		public long PanelImageId { get; set; }

		[Description("ProductId")]
		public string ProductId { get; set; }

        [Description("商品値段")]
		public int ShopProductPrice { get; set; }

		[Description("特権詳細説明リスト")]
		public List<ShopContractPrivilegeDescription> ShopContractPrivilegeDescriptionList { get; set; }

        [Description("デイリー報酬")]
		public List<UserItem> UserItemDailyList { get; set; }

        [Description("購入時報酬")]
		public UserItem UserItem { get; set; }
    }
}
