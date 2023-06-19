using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("バフ・デバフ効果グループ種別")]
	public enum EffectGroupType
	{
		[Description("なし")]
		None,
		[Description("スタン")]
		Stun
	}
}
