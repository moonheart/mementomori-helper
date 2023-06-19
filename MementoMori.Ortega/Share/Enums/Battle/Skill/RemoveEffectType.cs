using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("効果削除種別")]
	public enum RemoveEffectType
	{
		[Description("ターンの終わり")]
		TurnCountEnd,
		[Description("ターンの終わりとダメージを受けたとき")]
		TurnCountEndAndReceiveDamage
	}
}
