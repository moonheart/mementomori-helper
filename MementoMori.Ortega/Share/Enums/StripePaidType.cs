using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("Stripe支払いタイプ")]
	public enum StripePaidType
	{
		[Description("クレジットカード")]
		Card,
		[Description("アップルペイ")]
		ApplePay,
		[Description("グーグルペイ")]
		GooglePay
	}
}
