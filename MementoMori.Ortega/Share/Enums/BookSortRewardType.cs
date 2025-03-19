using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("書庫整理報酬タイプ")]
	public enum BookSortRewardType
	{
		[Description("None")]
		None,
		[Description("通常フロア当たりマス報酬")]
		Win,
		[Description("外れマス確定ドロップ報酬")]
		LoseFixed,
		[Description("外れマス低確率ドロップ報酬")]
		LoseRare
	}
}
