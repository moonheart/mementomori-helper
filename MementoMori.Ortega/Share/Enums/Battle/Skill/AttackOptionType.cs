using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("攻撃オプション種別")]
	public enum AttackOptionType
	{
		[Description("シールド類無視")]
		IgnoreShieldType = 1,
		[Description("必中")]
		PerfectHit,
		[Description("必ずクリティカル")]
		PerfectCritical,
		[Description("クリティカル不可")]
		NoneCritical,
		[Description("被ダメージ減少無視")]
		IgnoreReceiveDamageDown,
		[Description("被ダメージ基準値減少無視")]
		IgnoreReceiveDamageStandardDown,
		[Description("被ダメージ減少A（乗算）無視")]
		IgnoreReceiveDamageATypeDown,
		[Description("ダメージ無効無視")]
		IgnoreDamageGuard,
		[Description("シールド無視")]
		IgnoreShield1,
		[Description("多重バリア無視")]
		IgnoreShield2,
		[Description("結界無視")]
		IgnoreDamageBlock,
		[Description("遮断無視")]
		IgnoreCheckReceiveDamage
	}
}
