using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("マイページアイコン表示場所タイプ")]
	public enum MypageIconDisplayLocationType
	{
		[Description("マイページのみ")]
		MypageOnly,
		[Description("イベントポータルのみ")]
		EventPortalOnly,
		[Description("マイページとイベントポータル")]
		MypageAndEventPortal,
		[Description("サブイベントポータルのみ")]
		SubEventPortalOnly,
		[Description("マイページとサブイベントポータル")]
		MypageAndSubEventPortal
	}
}
