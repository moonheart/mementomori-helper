using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	[Description("装飾データ")]
	public class DecorationData
	{
		[Description("アイコンID")]
		public long IconId { get; set; }

		[Description("装飾ID")]
		public long DecorationId { get; set; }

		[Description("カラー")]
		public string DecorationColor { get; set; }

        [Description("追加装飾ID")]
		public long DecorationSpecialId { get; set; }

		[Description("追加装飾カラー")]
		public string DecorationSpecialColor { get; set; }

        [Description("購入回数制限タイプ")]
		public ShopBuyLimitType ShopBuyLimitType { get; set; }
	}
}
