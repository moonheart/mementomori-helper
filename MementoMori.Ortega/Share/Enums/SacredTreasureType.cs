using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("神器タイプ")]
	public enum SacredTreasureType
	{
		[Description("神器ではない")]
		None = 0,
		[Description("魔装")]
		Matchless = 1,
		[Description("聖装")]
		Legend = 2,
		[Description("双ステータス神器")]
		DualStatus = 3
	}
}
