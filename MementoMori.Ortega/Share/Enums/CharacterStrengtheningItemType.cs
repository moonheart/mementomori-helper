using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("キャラクター強化アイテムのタイプ")]
	public enum CharacterStrengtheningItemType
	{
		[Description("C級用強化アイテム")]
		C = 1,
		[Description("B級用強化アイテム")]
		B,
		[Description("A級用強化アイテム")]
		A
	}
}
