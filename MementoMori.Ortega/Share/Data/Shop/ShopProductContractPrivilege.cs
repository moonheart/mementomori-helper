using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductContractPrivilege
	{
		[Description("特権概要説明")]
		public string DescriptionContractPrivilege { get; set; }

        [Description("定期購読の説明")]
		public string DescriptionSubscription { get; set; }

        [Description("ダイアログ画像ID")]
		public long DialogImageId { get; set; }

		[Description("有効期限")]
		public long ExpirationTimeStamp { get; set; }

		[Description("無料トライアルを既に使用しているか")]
		public bool IsAlreadyUsedTrial { get; set; }

		[Description("パネル画像ID")]
		public long PanelImageId { get; set; }

		[Description("1週間購入ボタンのProductId")]
		public string ProductIdWeekly { get; set; }

        [Description("1週間購入の値段")]
		public int ShopProductPriceWeekly { get; set; }

		[Description("1カ月購入ボタンのProductId")]
		public string ProductIdMonthly { get; set; }

        [Description("1カ月購入の値段")]
		public int ShopProductPriceMonthly { get; set; }

		[Description("特権詳細説明リスト")]
		public List<ShopContractPrivilegeDescription> ShopContractPrivilegeDescriptionList { get; set; }
    }
}
