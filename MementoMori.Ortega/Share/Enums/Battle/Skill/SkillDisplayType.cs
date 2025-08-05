using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("スキル演出タイプ")]
	public enum SkillDisplayType
	{
		[Description("なし")]
		None,
		[Description("回復")]
		Heal,
		[Description("物理攻撃")]
		PhysicalAttack,
		[Description("魔法攻撃")]
		MagicAttack,
		[Description("物理 (直接攻撃)")]
		PhysicalDirectDamage,
		[Description("魔法 (直接攻撃)")]
		MagicDirectDamage,
		[Description("Hp吸収")]
		HpDrain,
		[Description("バフ")]
		Buff,
		[Description("デバフ")]
		DeBuff,
		[Description("物理攻撃カウンタ")]
		PhysicalCounterAttack,
		[Description("魔法攻撃カウンタ")]
		MagicCounterAttack,
		[Description("物理攻撃ダメージ連携")]
		PhysicalResonanceAttack,
		[Description("魔法攻撃ダメージ連携")]
		MagicResonanceAttack,
		[Description("効果削除")]
		RemoveEffect,
		[Description("即時発動")]
		BurstEffect,
		[Description("自傷ダメージ")]
		SelfInjuryDamage,
		[Description("復活")]
		Resurrection = 20,
		[Description("回復(演出なし)")]
		SilenceHeal = 101
	}
}
