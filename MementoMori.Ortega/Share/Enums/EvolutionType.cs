using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("武具進化の種類")]
	public enum EvolutionType
	{
		[Description("レベル最大値上昇")]
		ReinforcementLevelMaximumUp,
		[Description("レアリティ上昇")]
		RarityUp
	}
}
