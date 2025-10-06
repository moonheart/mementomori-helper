using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum ReviewGuidanceActionType
	{
		[Description("操作なし")]
		None,
		[Description("不満")]
		Negative,
		[Description("レビュー作成")]
		WriteReview,
		[Description("レビュースキップ")]
		ReviewSkipped
	}
}
