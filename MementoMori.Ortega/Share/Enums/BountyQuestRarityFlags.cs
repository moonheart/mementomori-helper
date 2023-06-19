using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Flags]
	[Description("祈りの泉クエストレアリティ")]
	public enum BountyQuestRarityFlags
	{
		[Description("None")]
		None = 0,
		[Description("N*")]
		NInit = 1,
		[Description("N")]
		N = 2,
		[Description("R")]
		R = 4,
		[Description("SR")]
		SR = 8,
		[Description("SSR")]
		SSR = 16,
		[Description("UR")]
		UR = 32,
		[Description("LR")]
		LR = 64
	}
}
