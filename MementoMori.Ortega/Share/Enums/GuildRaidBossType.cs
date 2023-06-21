using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum GuildRaidBossType
	{
		[Description("通常ボス")]
		Normal,
		[Description("解放ボス")]
		Releasable,
		[Description("イベントボス")]
		Event
	}
}
