using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.ServerLib.Models.MySql.Dto;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductFirstChargeBonus
	{
		[Description("詳細ダイアログキャラクターID")]
		public long CharacterId { get; set; }

		[Description("商品パネルのベース画像ID")]
		public long PanelImageId { get; set; }

		[Description("1日目の報酬")]
		public List<ShopItem> ShopItemList1 { get; set; }

        [Description("2日目の報酬")]
		public List<ShopItem> ShopItemList2 { get; set; }

        [Description("3日目の報酬")]
		public List<ShopItem> ShopItemList3 { get; set; }

        [Description("受取状況(未開放の場合はnull)")]
		public UserShopFirstChargeBonusDtoInfo UserShopFirstChargeBonusDtoInfo { get; set; }
    }
}
