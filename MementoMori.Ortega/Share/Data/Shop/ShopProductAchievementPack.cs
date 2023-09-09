using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.ServerLib.Models.MySql.Dto;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductAchievementPack
	{
		[Description("ダイアログベース画像ID")]
		public long DialogImageId { get; set; }

		[Description("終了日時")]
		public string EndTime { get; set; }

        [Description("獲得ダイヤ量")]
		public int GetCurrencyCount { get; set; }

		[Description("開放されているか")]
		public bool IsOpen { get; set; }

		[Description("商品名キー")]
		public string NameKey { get; set; }

        [Description("パネルベース画像ID")]
		public long PanelImageId { get; set; }

		[Description("ProductId")]
		public string ProductId { get; set; }

        [Description("払い戻し倍率")]
		public int RefundRate { get; set; }

		[Description("報酬リスト")]
		public List<ShopAchievementInfo> ShopAchievementInfoList { get; set; }

        [Description("商品値段")]
		public int ShopProductPrice { get; set; }

		[Description("詳細ダイアログの概要説明キー")]
		public string SummaryKey { get; set; }

        [Description("シンボル画像ID")]
		public long SymbolImageId { get; set; }

		[Description("開放報酬")]
		public UserItem UserItem { get; set; }

        [Description("受取情報")]
		public List<UserShopAchievementPackDtoInfo> UserShopAchievementPackDtoInfoList { get; set; }
    }
}
