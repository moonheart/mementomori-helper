using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("属性ボーナス発動段階タイプ")]
	public enum ElementBonusPhaseType
	{
		[Description("None")]
		None,
		[Description("段階1")]
		First,
		[Description("段階2")]
		Second,
		[Description("段階3")]
		Third,
		[Description("段階4")]
		Fourth,
		[Description("段階5")]
		Fifth
	}
}
