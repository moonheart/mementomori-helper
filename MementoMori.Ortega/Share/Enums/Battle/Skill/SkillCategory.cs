using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("スキルカテゴリー")]
	public enum SkillCategory
	{
		[Description("回復")]
		Heal = 1,
		[Description("バフ")]
		Buff,
		[Description("デバフ")]
		DeBuff,
		[Description("固有バフ")]
		SpecialBuff,
		[Description("固有デバフ")]
		SpecialDeBuff,
		[Description("物理攻撃")]
		PhysicalAttack = 10,
		[Description("魔法攻撃")]
		MagicAttack,
		[Description("物理 (防御無視)")]
		PhysicalNoDefense,
		[Description("魔法 (防御無視)")]
		MagicNoDefense,
		[Description("物理 (直接攻撃)")]
		PhysicalDirectDamage,
		[Description("魔法 (直接攻撃)")]
		MagicDirectDamage,
		[Description("物理 (固定攻撃)")]
		PhysicalFixDamage,
		[Description("魔法 (固定攻撃)")]
		MagicFixDamage,
		[Description("自傷ダメージ")]
		SelfInjuryDamage,
		[Description("復活")]
		Resurrection = 50,
		[Description("回復（影響無効）")]
		SilenceHeal,
		[Description("ステータス吸収")]
		StatusDrain = 100,
		[Description("印の効果")]
		SkillMark = 200,
		[Description("固有バフ（エフェクトなし）")]
		SpecialBuffNotEffect,
		[Description("固有デバフ（エフェクトなし）")]
		SpecialDeBuffNotEffect,
		[Description("バフ効果削除")]
		RemoveBuffEffect = 500,
		[Description("デバフ効果削除")]
		RemoveDebuffEffect,
		[Description("効果削除(エフェクトなし)")]
		RemoveOtherEffect,
		[Description("バフ移動")]
		BuffTransfer = 600,
		[Description("デバフ移動")]
		DeBuffTransfer,
		[Description("即時発動(バフ用)")]
		BurstBuffEffect = 1000,
		[Description("即時発動(デバフ用)")]
		BurstDeBuffEffect
	}
}
