using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopTabInfo
	{
		[Description("レイアウト")]
		public CustomTextLayout CustomTextLayout { get; set; }

        [Description("装飾データ")]
		public DecorationData DecorationData { get; set; }

        [Description("表示順(昇順)")]
		public int DisplayOrder { get; set; }

		[Description("Id")]
		public long Id { get; set; }

		[Description("タブ画像ID")]
		public long ImageId { get; set; }

		[Description("ダイヤタブかどうか")]
		public bool IsCurrencyTab { get; set; }

		[Description("タブ名キー")]
		public string NameKey { get; set; }

        [Description("商品一覧")]
		public List<ShopProductInfo> ShopProductInfoList { get; set; }

        [Description("Newバッジ表示対象かどうか")]
        public bool IsNewBadgeTarget { get; set; }

        [Description("Newバッジ表示フラグ")]
        public bool IsNew { get; set; }
    }
}
