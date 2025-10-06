using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("人気投票形式タイプ")]
	public enum PopularityVoteVotingType
	{
		[Description("なし")]
		None,
		[Description("シンプル投票形式")]
		Simple,
		[Description("グループ投票形式")]
		Group
	}
}
