using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("動画再生機能全般の遷移元情報")]
	public enum PlayVideoTransitionSourceType
	{
		[Description("なし")]
		None,
		[Description("マイページ")]
		MyPage,
		[Description("イベントポータル/サブイベントポータル")]
		EventPortal
	}
}
