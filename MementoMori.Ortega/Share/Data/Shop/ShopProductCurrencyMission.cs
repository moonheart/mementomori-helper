using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.ServerLib.Models.MySql.Dto;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductCurrencyMission
	{
		[Description("現在のポイント")]
		public long CurrentPoint { get; set; }

		[Description("ダイアログベース画像ID")]
		public long DialogImageId { get; set; }

		[Description("ミッションの終了日時")]
		public string EndDateTime { get; set; }

        [Description("開放についての説明キー")]
		public string ExplanationKey { get; set; }

        [Description("PT購入ボタン表示するか")]
		public bool IsDisplayBuyPointButton { get; set; }

		[Description("開放報酬")]
		public bool IsPremium { get; set; }

		[Description("商品名キー")]
		public string NameKey { get; set; }

        [Description("パネルベース画像ID")]
		public long PanelImageId { get; set; }

		[Description("ProductId")]
		public string ProductId { get; set; }

        [Description("商品値段")]
		public int ShopProductPrice { get; set; }

		[Description("目標リスト")]
		public List<ShopCurrencyMissionInfo> ShopCurrencyMissionInfoList { get; set; }

        [Description("課金機能付きミッションの種類")]
		public ShopCurrencyMissionType ShopCurrencyMissionType { get; set; }

		[Description("詳細ダイアログの概要説明キー")]
		public string SummaryKey { get; set; }

        [Description("開放報酬")]
		public UserItem UserItem { get; set; }

        [Description("受取状況リスト")]
		public List<UserShopCurrencyMissionRewardDtoInfo> UserShopCurrencyMissionRewardDtoInfoList { get; set; }
    }
}
