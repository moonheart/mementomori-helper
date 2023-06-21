using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Flags]
	[Description("高速周回チケット報酬")]
	public enum QuestQuickTicketRewardFlags
	{
		[Description("不明")]
		None = 0,
		[Description("ゴールド")]
		Gold = 1,
		[Description("経験珠")]
		CharacterExp = 2,
		[Description("潜在宝珠")]
		Seed = 4
	}
}
