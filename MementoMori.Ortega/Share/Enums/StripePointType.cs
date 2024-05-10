using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("Stripeポイント増減タイプ")]
	public enum StripePointType
	{
		[Description("商品購入")]
		BuyProduct,
		[Description("補填")]
		Present,
		[Description("回収")]
		Retrieve
	}
}
