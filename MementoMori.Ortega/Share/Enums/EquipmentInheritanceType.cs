using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("武具継承項目")]
	public enum EquipmentInheritanceType
	{
		[Description("None")]
		None,
		[Description("強化Lv")]
		ReinforcementLv,
		[Description("魔装Lv")]
		MatchlessSacredTreasureLv,
		[Description("聖装LV")]
		LegendSacredTreasureLv,
		[Description("ルーン")]
		Sphere
	}
}
