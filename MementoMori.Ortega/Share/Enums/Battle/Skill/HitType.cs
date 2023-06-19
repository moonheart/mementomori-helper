using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("命中種別")]
	public enum HitType
	{
		[Description("無視")]
		Ignore,
		[Description("命中")]
		Hit,
		[Description("ミス")]
		Miss,
		[Description("クリティカル")]
		Critical,
		[Description("シールド1基本")]
		Shield1,
		[Description("シールド1クリティカル")]
		Shield1Critical,
		[Description("シールド2基本")]
		Shield2,
		[Description("シールド2クリティカル")]
		Shield2Critical,
		[Description("シールド破壊")]
		ShieldBreak,
		[Description("シールド破壊クリティカル")]
		ShieldBreakCritical
	}
}
