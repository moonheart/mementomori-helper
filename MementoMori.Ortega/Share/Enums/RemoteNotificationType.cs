using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("リモート通知種別")]
	public enum RemoteNotificationType
	{
		None,
		[Description("ギルドチャット")]
		GuildChat,
		[Description("個人チャット")]
		PersonalChat,
		[Description("ギルドレイドボスの開放")]
		GuildRaidBoss,
		[Description("イベント")]
		Event,
		[Description("不具合補填")]
		Compensation,
		[Description("リテンション")]
		Retention
	}
}
