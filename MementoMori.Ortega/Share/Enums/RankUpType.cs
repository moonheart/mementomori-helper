using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ランクアップ·タイプ")]
	public enum RankUpType
	{
		[Description("None")]
		None,
		[Description("同一属性")]
		ElementType,
		[Description("同じキャラクターID")]
		SameName
	}
}
