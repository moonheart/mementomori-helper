using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("効果削除種別")]
	public enum RemoveEffectType
	{
		[Description("ターンの終わり")]
		TurnCountEnd,
		[Description("ターンの終わりとダメージを受けたとき")]
		TurnCountEndAndReceiveDamage,
		[Description("付帯者の行動終了時")]
		ActionEnd,
		[Description("ターンの終わりと付与者の戦闘不能時")]
		TurnCountEndAndGranterDead
	}
}
