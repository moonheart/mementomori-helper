using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ローカル通知種別")]
	public enum LocalNotificationSendType
	{
		None,
		[Description("時刻指定")]
		TimeSpecified,
		[Description("放置報酬上限到達")]
		AutoBattle
	}
}
