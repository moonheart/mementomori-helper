using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ガチャセレクトリストタイプ")]
	public enum GachaSelectListType
	{
		[Description("なし")]
		None,
		[Description("プライズ共通")]
		Default,
		[Description("運命")]
		Destiny,
		[Description("星の導き")]
		StarsGuidance,
		[Description("選択ピックアップ")]
		SelectablePickUp,
		[Description("スペシャルセレクト")]
		SpecialSelectablePickUp
	}
}
