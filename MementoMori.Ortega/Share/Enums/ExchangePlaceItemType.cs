using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("交換所アイテムタイプ")]
	public enum ExchangePlaceItemType
	{
		[Description("魔水晶")]
		CastingStone = 1,
		[Description("精錬鋼")]
		CastingValue,
		[Description("聖装鋼")]
		Fame,
		[Description("スフィアチケット")]
		SphereTicket,
		[Description("紅の金剛石")]
		YellowJade,
		[Description("天契の聖杯")]
		ChaliceOfHeavenly,
		[Description("蒼穹の銀勲")]
		SilverOrderOfTheBlueSky,
		[Description("希求の神翼")]
		DivineWingsOfDesire,
		[Description("悠園の果実")]
		FruitOfTheGarden,
		[Description("バトルコイン")]
		BattleCoin,
		[Description("レジェンドコイン")]
		LegendCoin,
		[Description("ギルドコイン")]
		GuildCoin,
		[Description("時空コイン")]
		DungeonCoin,
		[Description("キャラコイン")]
		CharacterCoin,
		[Description("グランドコイン")]
		GrandBattleCoin
	}
}
