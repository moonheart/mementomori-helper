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
		[Description("Live2Dのセット内連撃（5回未満）の演出")]
		UnderFiveLive2DInSubSet,
		[Description("Live2D後のセット内連撃（5回未満）の演出")]
		UnderFiveLive2DAfterInSubSet,
		[Description("Live2Dのセット内連撃（5回以上）の演出")]
		AboveFourLive2DInSubSet,
		[Description("Live2D後のセット内連撃（5回以上）の演出")]
		AboveFourLive2DAfterInSubSet,
		[Description("Live2Dのセット外連撃（5回未満）の演出")]
		UnderFiveLive2DOutOfSubSet,
		[Description("Live2D後のセット外連撃（5回未満）の演出")]
		UnderFiveLive2DAfterOutOfSubSet,
		[Description("Live2Dのセット外連撃（5回以上）の演出")]
		AboveFourLive2DOutOfSubSet,
		[Description("Live2D後のセット外連撃（5回以上）の演出")]
		AboveFourLive2DAfterOutOfSubSet
	}
}
