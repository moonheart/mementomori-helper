using System.ComponentModel;

namespace MementoMori.Ortega.Common.Enums
{
	[Description("神器タイプのフィルター")]
	[Flags]
	public enum SacredTreasureFilterFlags
	{
		[Description("魔装")]
		Matchless = 1,
		[Description("聖装")]
		Legend = 2,
		[Description("全て")]
		All = 3
	}
}
