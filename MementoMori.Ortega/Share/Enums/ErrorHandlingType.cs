using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("エラーに対する挙動")]
	public enum ErrorHandlingType
	{
		[Description("何もしない")]
		None,
		[Description("トーストを表示する")]
		Toast,
		[Description("ダイアログを表示する")]
		OpenErrorDialog,
		[Description("タイトルに戻るダイアログを表示する")]
		BackToTitle,
		[Description("MagicOnionの再接続を行う")]
		MagicOnionReconnect,
		[Description("MyPageに戻るダイアログを表示する")]
		BackToMyPage
	}
}
