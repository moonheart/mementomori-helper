using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum GuildRaidAutoOpenFailType
	{
		[Description("なし")]
		None,
		[Description("必要名声値不足")]
		NotEnoughGuildFame,
		[Description("１日の開始可能回数上限到達")]
		OverChallengeCount,
		[Description("前回開始されたボス未討伐")]
		NotClearBeforeBoss
	}
}
