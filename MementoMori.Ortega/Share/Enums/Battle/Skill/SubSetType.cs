using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums.Battle.Skill
{
	[Description("サブセットタイプ")]
	public enum SubSetType
	{
        [Description("Live2D前の演出")]
        Live2DBefore,
        [Description("Live2D基本演出")]
        DefaultLive2D,
        [Description("Live2D後の基本演出")]
        DefaultLive2DAfter,
        [Description("Live2Dのセット外連撃（5回未満）の演出")]
        UnderFiveLive2DOutOfSubSet = 7,
        [Description("Live2D後のセット外連撃（5回未満）の演出")]
        UnderFiveLive2DAfterOutOfSubSet,
        [Description("Live2Dのセット外連撃（5回以上）の演出")]
        AboveFourLive2DOutOfSubSet,
        [Description("Live2D後のセット外連撃（5回以上）の演出")]
        AboveFourLive2DAfterOutOfSubSet
	}
}
