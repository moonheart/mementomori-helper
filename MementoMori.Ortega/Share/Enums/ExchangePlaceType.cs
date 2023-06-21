using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("交換所タイプ")]
	public enum ExchangePlaceType
	{
		[Description("宝石")]
		Sphere = 1,
		[Description("製造")]
		Manufacturing,
		[Description("名声")]
		Fame,
		[Description("闘技場")]
		Arena,
		[Description("同盟")]
		Alliance,
		[Description("皇室")]
		ImperialFamily,
		[Description("専属武器")]
		ExclusiveWeapon,
		[Description("神将")]
		GodGeneral,
		[Description("紅翡翠")]
		RedJade,
		[Description("黄翡翠")]
		YellowJade,
		[Description("ガチャ")]
		Gacha,
		[Description("出会い")]
		Encounter
	}
}
