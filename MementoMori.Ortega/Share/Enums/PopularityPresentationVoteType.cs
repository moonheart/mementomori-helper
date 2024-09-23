using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("発表タイプ")]
	public enum PopularityPresentationVoteType
	{
		[Description("なし")]
		None,
		[Description("予選中間発表")]
		PreliminaryInterim,
		[Description("予選結果")]
		PreliminaryResult,
		[Description("本選中間発表")]
		FinalInterim,
		[Description("本選結果")]
		FinalResult
	}
}
