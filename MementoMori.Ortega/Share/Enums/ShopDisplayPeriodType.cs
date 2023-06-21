using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("表示期間条件タイプ")]
	public enum ShopDisplayPeriodType
	{
		[Description("常に表示")]
		Always,
		[Description("期間指定")]
		Period,
		[Description("ゲーム登録日からの経過日数")]
		GameStart,
		[Description("カムバック日からの経過日数")]
		ComeBack
	}
}
