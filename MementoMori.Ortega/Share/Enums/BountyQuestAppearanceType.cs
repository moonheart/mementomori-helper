using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("懸賞カウンタークエスト出現タイプ")]
	public enum BountyQuestAppearanceType
	{
		[Description("日常")]
		EveryTime,
		[Description("4時から7時まで")]
		Time4to7,
		[Description("7時から10時まで")]
		Time7to10,
		[Description("10時から13時まで")]
		Time10to13,
		[Description("13時から16時まで")]
		Time13to16,
		[Description("16時から19時まで")]
		Time16to19,
		[Description("19時から22時まで")]
		Time19to22,
		[Description("22時から1時まで")]
		Time22to1,
		[Description("1時から4時まで")]
		Time1to4
	}
}
