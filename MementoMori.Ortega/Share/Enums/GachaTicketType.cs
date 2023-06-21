using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ガチャチケットタイプ")]
	public enum GachaTicketType
	{
		[Description("聖天使の神託チケット")]
		HolyAngel = 1,
		[Description("プラチナガチャチケット")]
		Platinum,
		[Description("属性ガチャチケット")]
		Element,
		[Description("運命ガチャ")]
		Destiny,
		[Description("黒葬武具ガチャチケット")]
		Sinful,
		[Description("天光武具ガチャチケット")]
		Holy,
		[Description("禁忌武具ガチャチケット")]
		Forbidden,
		[Description("スタートダッシュガチャチケット")]
		StartDash,
		[Description("ピックアップガチャチケット")]
		PickUp
	}
}
