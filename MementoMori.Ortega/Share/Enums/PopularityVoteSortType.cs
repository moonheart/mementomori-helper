using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("人気投票キャラソートタイプ")]
	public enum PopularityVoteSortType
	{
		[Description("なし")]
		None,
		[Description("キャラID順")]
		Id,
		[Description("ランダム順")]
		Random
	}
}
