using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ランクアップ·タイプ")]
	public enum ElementClassificationType
	{
		[Description("None")]
		None,
		[Description("基本属性")]
		DefaultElement,
		[Description("特殊属性")]
		SpecialElement
	}
}
