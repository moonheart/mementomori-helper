using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("効果オプション種別")]
	public enum EffectOptionType
	{
		[Description("なし")]
		None,
		[Description("弱体効果命中判定無視")]
		IgnoreDebuffHitCheck
	}
}
