using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("ダメージ系効果種別")]
	public enum HpEffectType
	{
		[Description("効果なし")]
		None,
		[Description("自分が「狂乱」状態、会心率100%")]
		CriticalHitWhenBerserker,
		[Description("自分のHPが半分未満ならばダメージが〇％分増加")]
		DamageUpByAttackerHpLessHalf,
		[Description("対象のHP最大値から減少するごとにダメージが〇％分増加")]
		DamageUpByTargetHpPercent,
		[Description("対象のHP上限の〇％分加算")]
		DamagePlusByTargetHpPercent,
		[Description("自分の筋力が対象より高い場合、ダメージが〇％分増加")]
		DamageUpByBetterMuscle,
		[Description("自身の残りMPの〇倍分加算")]
		DamagePlusBySelfRemainMp,
		[Description("対象が「凍結」状態しダメージ増加")]
		DamageUpByTargetFreezingEffect = 500,
		[Description("対象が「毒」状態しダメージ増加")]
		DamageUpByTargetPoisonEffect,
		[Description("HP吸収")]
		HpDrain = 1000
	}
}
