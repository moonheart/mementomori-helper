using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("時差グループ")]
	public enum TimeServerType
	{
		None,
		[Description("日本")]
		JP,
		[Description("韓国")]
		KR,
		[Description("アジア")]
		Asia,
		[Description("アメリカ")]
		US,
		[Description("ヨーロッパ")]
		EU,
		[Description("グローバル")]
		Global
	}
}
