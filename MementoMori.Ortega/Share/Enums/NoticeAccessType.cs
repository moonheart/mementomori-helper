using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("お知らせをどこから開いたか")]
	public enum NoticeAccessType
	{
		[Description("不明")]
		None,
		[Description("タイトル")]
		Title,
		[Description("マイページ")]
		MyPage
	}
}
