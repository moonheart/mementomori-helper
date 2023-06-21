using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("宝箱抽選タイプ")]
	public enum TreasureChestLotteryType
	{
		[Description("抽選で1つ")]
		Random,
		[Description("固定で1つ")]
		Static,
		[Description("キャラクター1つを選択")]
		SelectCharacter,
		[Description("アイテム1つを選択")]
		SelectItem,
		[Description("選択した中から抽選")]
		SelectRandom,
		[Description("複数の固定アイテム")]
		Fix,
		[Description("抽選アイテムと固定アイテム")]
		RandomFix
	}
}
