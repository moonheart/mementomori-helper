using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("LocalGvGの、城の状態")]
	public enum GvgCastleState
	{
		[Description("通常時")]
		None,
		[Description("宣戦され、交戦中")]
		InBattle,
		[Description("陥落した")]
		Fallen,
		[Description("反撃")]
		InCounter,
		[Description("反撃に成功した")]
		CounterSuccess
	}
}
