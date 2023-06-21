using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("購入回数制限タイプ")]
	public enum ShopBuyLimitType
	{
		[Description("購入回数制限なし")]
		None,
		[Description("永久の購入回数制限")]
		Forever,
		[Description("日単位の購入回数制限")]
		Daily,
		[Description("週単位の購入回数制限")]
		Weekly,
		[Description("月単位の購入回数制限")]
		Monthly,
		[Description("指定された経過日数が経過し、その商品が閉じるときにリセット")]
		EndDisplayProduct
	}
}
