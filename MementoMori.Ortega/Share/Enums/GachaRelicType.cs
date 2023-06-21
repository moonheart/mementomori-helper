using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ガチャ聖遺物タイプ")]
	public enum GachaRelicType
	{
		None,
		[Description("天契の聖杯")]
		ChaliceOfHeavenly,
		[Description("蒼穹の銀勲")]
		SilverOrderOfTheBlueSky,
		[Description("希求の神翼")]
		DivineWingsOfDesire,
		[Description("悠園の果実")]
		FruitOfTheGarden
	}
}
