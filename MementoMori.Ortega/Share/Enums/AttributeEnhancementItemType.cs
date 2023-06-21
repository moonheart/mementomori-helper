using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("属性強化アイテムの種類")]
	public enum AttributeEnhancementItemType
	{
		[Description("かわいさ")]
		Cute = 1,
		[Description("セクシー")]
		Sexy,
		[Description("癒やし")]
		Healing,
		[Description("活発")]
		Active,
		[Description("幸運")]
		Happiness,
		[Description("聡明")]
		Wisdom
	}
}
