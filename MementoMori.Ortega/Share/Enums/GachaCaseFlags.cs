using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ガチャ表示用フラグ")]
	[Flags]
	public enum GachaCaseFlags
	{
		[Description("なし")]
		None = 0,
		[Description("天井表示")]
		ShowCeilingCount = 1,
		[Description("レビュー誘導判定無し")]
		IgnoreReview = 2,
		[Description("シェアボタン表示なし")]
		HideShareButton = 4,
		[Description("ガチャ詳細ダイアログ スペシャルリスト非表示")]
		HideSpecialList = 8
	}
}
