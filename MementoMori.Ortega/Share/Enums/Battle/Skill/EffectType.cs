using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("バフ・デバフ効果種別")]
	public enum EffectType
	{
		[Description("なし")]
		None,
		[Description("スピード増加")]
		SpeedUp = 1001,
		[Description("最大HP増加")]
		MaxHpUp,
		[Description("攻撃力増加")]
		AttackPowerUp,
		[Description("防御力増加")]
		DefenseUp,
		[Description("物理ダメージ緩和増加")]
		PhysicalDamageRelaxUp,
		[Description("魔法ダメージ緩和増加")]
		MagicDamageRelaxUp,
		[Description("防御貫通力増加")]
		DefensePenetrationUp,
		[Description("ダメージ強化増加")]
		DamageEnhanceUp,
		[Description("命中増加")]
		HitUp,
		[Description("回避増加")]
		AvoidanceUp,
		[Description("クリティカル増加")]
		CriticalUp,
		[Description("クリティカル耐性増加")]
		CriticalResistUp,
		[Description("HP吸収増加")]
		HpDrainUp,
		[Description("カウンタ増加")]
		DamageReflectUp,
		[Description("クリダメ強化増加")]
		CriticalDamageEnhanceUp,
		[Description("物理クリダメ緩和増加")]
		PhysicalCriticalDamageRelaxUp,
		[Description("魔法クリダメ緩和増加")]
		MagicCriticalDamageRelaxUp,
		[Description("弱体効果命中増加")]
		DebuffHitUp,
		[Description("弱体効果耐性増加")]
		DebuffResistUp,
		[Description("与HP回復量増加")]
		GiveHealRateUp,
		[Description("被HP回復量増加")]
		ReceiveHealRateUp,
		[Description("与ダメージ増加")]
		GiveDamageUp,
		[Description("被ダメージ減少")]
		ReceiveDamageDown,
		[Description("スキルクールダウン加速")]
		CoolTimeRecoveryUp,
		[Description("属性ダメージボーナス増加")]
		ElementBonusUp,
		[Description("与ダメージ基準値増加")]
		GiveDamageStandardUp,
		[Description("被ダメージ基準値減少")]
		ReceiveDamageStandardDown,
		[Description("被持続ダメージ減少")]
		ReceiveTransientDamageDown,
		[Description("命中率増加")]
		HitRateUp = 1500,
		[Description("回避率増加")]
		AvoidanceRateUp,
		[Description("クリティカル率増加")]
		CriticalRateUp,
		[Description("クリティカル耐性率増加")]
		CriticalResistRateUp,
		[Description("弱体効果命中率増加")]
		DebuffHitRateUp,
		[Description("弱体効果耐性率増加")]
		DebuffResistRateUp,
		[Description("攻撃力増加(A)")]
		AttackPowerATypeUp = 1610,
		[Description("与ダメージ増加(A)")]
		GiveDamageATypeUp = 1620,
		[Description("被ダメージ減少(A)")]
		ReceiveDamageATypeDown = 1630,
		[Description("ダメージ無効化")]
		DamageGuard = 2001,
		[Description("シールド1")]
		Shield1,
		[Description("シールド2")]
		Shield2,
		[Description("デバフ無効化")]
		DebuffGuard,
		[Description("行動阻害無効化")]
		ConfuseActionDebuffGuard,
		[Description("挑発")]
		Taunt,
		[Description("潜伏")]
		Stealth,
		[Description("除外")]
		NonTarget,
		[Description("デバフ付与")]
		GiveDebuff,
		[Description("通常攻撃強化")]
		NormalSkillEnhance = 2011,
		[Description("毎ターン回復")]
		HealOverTime,
		[Description("必ず回避")]
		NonHit,
		[Description("不死身")]
		Immortal,
		[Description("印")]
		SkillMark,
		[Description("攻撃ダメージ遮断")]
		DamageBlock,
		[Description("持続ダメージ遮断")]
		TransientDamageBlock,
		[Description("バフカバー")]
		BuffCover,
		[Description("指定作用無効")]
		NegateEffect,
		[Description("スキル1強化")]
		ActiveSkill1Enhance = 2100,
		[Description("スキル2強化")]
		ActiveSkill2Enhance,
		[Description("カウンタ変更11")]
		DamageReflectEnhance11 = 2111,
		[Description("カウンタ変更12")]
		DamageReflectEnhance12,
		[Description("カウンタ変更13")]
		DamageReflectEnhance13,
		[Description("カウンタ変更14")]
		DamageReflectEnhance14,
		[Description("カウンタ変更15")]
		DamageReflectEnhance15,
		[Description("カウンタ変更16")]
		DamageReflectEnhance16,
		[Description("カウンタ変更17")]
		DamageReflectEnhance17,
		[Description("カウンタ変更18")]
		DamageReflectEnhance18,
		[Description("カウンタ変更21")]
		DamageReflectEnhance21 = 2121,
		[Description("カウンタ変更22")]
		DamageReflectEnhance22,
		[Description("カウンタ変更23")]
		DamageReflectEnhance23,
		[Description("カウンタ変更24")]
		DamageReflectEnhance24,
		[Description("カウンタ変更25")]
		DamageReflectEnhance25,
		[Description("カウンタ変更26")]
		DamageReflectEnhance26,
		[Description("カウンタ変更27")]
		DamageReflectEnhance27,
		[Description("カウンタ変更28")]
		DamageReflectEnhance28,
		[Description("カウンタ変更31")]
		DamageReflectEnhance31 = 2131,
		[Description("カウンタ変更32")]
		DamageReflectEnhance32,
		[Description("カウンタ変更33")]
		DamageReflectEnhance33,
		[Description("カウンタ変更34")]
		DamageReflectEnhance34,
		[Description("カウンタ変更35")]
		DamageReflectEnhance35,
		[Description("カウンタ変更36")]
		DamageReflectEnhance36,
		[Description("カウンタ変更37")]
		DamageReflectEnhance37,
		[Description("カウンタ変更38")]
		DamageReflectEnhance38,
		[Description("全てのスキルのクールタイムを回復")]
		AllSkillCoolTimeRecovery = 3001,
		[Description("スキル1のクールタイムを回復")]
		Skill1CoolTimeRecovery,
		[Description("スキル2のクールタイムを回復")]
		Skill2CoolTimeRecovery,
		[Description("全てのスキルのクールタイムを増加")]
		AllSkillCoolTimeIncrease,
		[Description("スキル1のクールタイムを増加")]
		Skill1CoolTimeIncrease,
		[Description("スキル2のクールタイムを増加")]
		Skill2CoolTimeIncrease,
		[Description("ランダムなバフ効果のターン数を増やす")]
		ExtendRandomBuffTurn = 3031,
		[Description("ランダムなデバフ効果のターン数を増やす")]
		ExtendRandomDeBuffTurn,
		[Description("ランダムなバフ効果のターン数を減らす")]
		ReduceRandomBuffTurn,
		[Description("ランダムなデバフ効果のターン数を減らす")]
		ReduceRandomDeBuffTurn,
		[Description("すべてのバフ効果のターンを増やす")]
		ExtendAllBuffTurn = 3041,
		[Description("すべてのデバフ効果のターンを増やす")]
		ExtendAllDeBuffTurn,
		[Description("すべてのスタン効果のターンを増やす")]
		ExtendStunTurn,
		[Description("すべてのバフ効果のターンを減らす")]
		ReduceAllBuffTurn,
		[Description("すべてのデバフ効果のターンを減らす")]
		ReduceAllDeBuffTurn,
		[Description("指定した効果グループのターン数を増やす")]
		ExtendEffectGroup,
		[Description("指定した効果グループのターン数を減らす")]
		ReduceEffectGroup,
		[Description("指定したEffectのスタック数を増やす")]
		IncreaseEffectStack,
		[Description("指定したEffectのスタック数を減らす")]
		DecreaseEffectStack,
		[Description("全てのバフを解除")]
		RemoveAllBuff,
		[Description("全てのデバフを解除")]
		RemoveAllDeBuff = 3060,
		[Description("全ての行動阻害デバフを解除")]
		RemoveAllConfuseActionGroupDebuff,
		[Description("同じグループ効果を解除")]
		RemoveEffectGroup = 3101,
		[Description("同じタイプの効果をすべて解除")]
		RemoveEffectType,
		[Description("バフを解除")]
		RemoveBuff,
		[Description("デバフを解除")]
		RemoveDeBuff,
		[Description("同じタイプの固有効果をすべて解除")]
		RemoveSpecialEffectType,
		[Description("アーカイブバフをコピー")]
		CopyArchiveBuff = 3200,
		[Description("アーカイブデバフをコピー")]
		CopyArchiveDeBuff,
		[Description("ターゲットの全てのバフをコピー")]
		CopyAllBuffTargetToSelf,
		[Description("自分のすべてのデバフをターゲットにコピー")]
		CopyAllDeBuffSelfToTarget,
		[Description("敵のバフを奪う")]
		MoveBuffToMeFromEnemy,
		[Description("自分のデバフを敵に移す")]
		MoveDebuffToEnemyFromMe,
		[Description("ターゲットのバフを指定数コピー")]
		CopyBuffTargetToSelf,
		[Description("自分のデバフをターゲットに指定数コピー")]
		CopyDeBuffSelfToTarget,
		[Description("スピード減少")]
		SpeedDown = 5001,
		[Description("最大HP減少")]
		MaxHpDown,
		[Description("攻撃力減少")]
		AttackPowerDown,
		[Description("防御力減少")]
		DefenseDown,
		[Description("物理ダメージ緩和減少")]
		PhysicalDamageRelaxDown,
		[Description("魔法ダメージ緩和減少")]
		MagicDamageRelaxDown,
		[Description("防御貫通力減少")]
		DefensePenetrationDown,
		[Description("ダメージ強化減少")]
		DamageEnhanceDown,
		[Description("命中減少")]
		HitDown,
		[Description("回避減少")]
		AvoidanceDown,
		[Description("クリティカル減少")]
		CriticalDown,
		[Description("クリティカル耐性減少")]
		CriticalResistDown,
		[Description("HP吸収減少")]
		HpDrainDown,
		[Description("カウンタ減少")]
		DamageReflectDown,
		[Description("クリダメ強化減少")]
		CriticalDamageEnhanceDown,
		[Description("物理クリダメ緩和減少")]
		PhysicalCriticalDamageRelaxDown,
		[Description("魔法クリダメ緩和減少")]
		MagicCriticalDamageRelaxDown,
		[Description("弱体効果命中減少")]
		DebuffHitDown,
		[Description("弱体効果耐性減少")]
		DebuffResistDown,
		[Description("与HP回復量減少")]
		GiveHealRateDown,
		[Description("被HP回復量減少")]
		ReceiveHealRateDown,
		[Description("与ダメージ減少")]
		GiveDamageDown,
		[Description("被ダメージ増加")]
		ReceiveDamageUp,
		[Description("スキルクールダウン減少")]
		CoolTimeRecoveryDown,
		[Description("与ダメージ基準値減少")]
		GiveDamageStandardDown,
		[Description("被ダメージ基準値増加")]
		ReceiveDamageStandardUp,
		[Description("被持続ダメージ増加")]
		ReceiveTransientDamageUp = 5028,
		[Description("命中率減少")]
		HitRateDown = 5500,
		[Description("回避率減少")]
		AvoidanceRateDown,
		[Description("クリティカル率減少")]
		CriticalRateDown,
		[Description("クリティカル耐性率減少")]
		CriticalResistRateDown,
		[Description("弱体効果命中率減少")]
		DebuffHitRateDown,
		[Description("弱体効果耐性率減少")]
		DebuffResistRateDown,
		[Description("攻撃力減少(A)")]
		AttackPowerATypeDown = 5610,
		[Description("与ダメージ減少(A)")]
		GiveDamageATypeDown = 5620,
		[Description("被ダメージ増加(A)")]
		ReceiveDamageATypeUp = 5630,
		[Description("スタン")]
		Stun = 6001,
		[Description("混乱")]
		Confuse,
		[Description("封印")]
		Silence,
		[Description("固執")]
		Stubborn,
		[Description("HP回復不可")]
		HpRecoveryForbidden = 7002,
		[Description("バフ獲得不可")]
		BuffForbidden,
		[Description("回避不可")]
		AvoidanceForbidden,
		[Description("ロックオン11")]
		LockOnSelf = 7111,
		[Description("ロックオン21")]
		LockOnAllAlly = 7121,
		[Description("ロックオン31")]
		LockOnBlueAlly = 7131,
		[Description("ロックオン32")]
		LockOnRedAlly,
		[Description("ロックオン33")]
		LockOnGreenAlly,
		[Description("ロックオン34")]
		LockOnYellowAlly,
		[Description("ロックオン35")]
		LockOnLightAlly,
		[Description("ロックオン36")]
		LockOnDarkAlly,
		[Description("ロックオン41")]
		LockOnWarriorAlly = 7141,
		[Description("ロックオン42")]
		LockOnSniperAlly,
		[Description("ロックオン43")]
		LockOnSorcererAlly,
		[Description("ロックオン51")]
		LockOnAttack1Ally = 7151,
		[Description("ロックオン52")]
		LockOnAttack2Ally,
		[Description("ロックオン53")]
		LockOnAttack3Ally,
		[Description("毒")]
		Poison = 8001,
		[Description("出血")]
		Bleeding,
		[Description("浸食")]
		Combustion,
		[Description("火傷")]
		Burn,
		[Description("毒（自傷）")]
		SelfInjuryPoison = 8101,
		[Description("出血（自傷）")]
		SelfInjuryBleeding,
		[Description("浸食（自傷）")]
		SelfInjuryCombustion,
		[Description("ダメージ連携11")]
		DamageResonanceFromSelfAndDamageReduction = 8111,
		[Description("ダメージ連携21")]
		DamageResonanceFromHighHpEnemy = 8121,
		[Description("ダメージ連携22")]
		DamageResonanceFromLowHpEnemy,
		[Description("ダメージ連携23")]
		DamageResonanceFromHighDefenseEnemy,
		[Description("ダメージ連携24")]
		DamageResonanceFromLowDefenseEnemy,
		[Description("ダメージ連携25")]
		DamageResonanceFromRandomEnemy,
		[Description("ダメージ連携26")]
		DamageResonanceFromHighBaseMaxHpEnemy,
		[Description("ダメージ連携27")]
		DamageResonanceFromLowBaseMaxHpEnemy,
		[Description("ダメージ連携28")]
		DamageResonanceFromHighBaseDefenseEnemy,
		[Description("ダメージ連携29")]
		DamageResonanceFromLowBaseDefenseEnemy,
		[Description("ダメージ連携31")]
		DamageResonanceFromAllEnemy = 8131,
		[Description("ダメージ連携41")]
		DamageResonanceFromAllAllyAndDamageReduction = 8141,
		[Description("スピード吸収")]
		SpeedDrain = 9001,
		[Description("攻撃力吸収")]
		AttackPowerDrain = 9003,
		[Description("防御力吸収")]
		DefenseDrain
	}
}
