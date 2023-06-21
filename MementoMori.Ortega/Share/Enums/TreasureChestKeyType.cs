using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("宝箱の鍵")]
	public enum TreasureChestKeyType
	{
		[Description("初級封印の鍵")]
		Bronze = 1,
		[Description("中級封印の鍵")]
		Silver,
		[Description("上級封印の鍵")]
		Golden
	}
}
