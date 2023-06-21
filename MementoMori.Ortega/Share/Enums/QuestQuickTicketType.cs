using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("高速周回チケット")]
	public enum QuestQuickTicketType
	{
		[Description("ゴールド1時間")]
		GoldHours1 = 1,
		[Description("ゴールド2時間")]
		GoldHours2,
		[Description("ゴールド6時間")]
		GoldHours6,
		[Description("ゴールド8時間")]
		GoldHours8,
		[Description("ゴールド24時間")]
		GoldHours24,
		[Description("経験珠1時間")]
		ExpHours1,
		[Description("経験珠2時間")]
		ExpHours2,
		[Description("経験珠6時間")]
		ExpHours6,
		[Description("経験珠8時間")]
		ExpHours8,
		[Description("経験珠24時間")]
		ExpHours24,
		[Description("潜在宝珠1時間")]
		SeedHours1,
		[Description("潜在宝珠2時間")]
		SeedHours2,
		[Description("潜在宝珠6時間")]
		SeedHours6,
		[Description("潜在宝珠8時間")]
		SeedHours8,
		[Description("潜在宝珠24時間")]
		SeedHours24,
		[Description("豪華な袋1時間")]
		LuxuryHours1,
		[Description("豪華な袋2時間")]
		LuxuryHours2,
		[Description("豪華な袋6時間")]
		LuxuryHours6,
		[Description("豪華な袋8時間")]
		LuxuryHours8,
		[Description("豪華な袋24時間")]
		LuxuryHours24
	}
}
