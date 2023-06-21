using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ミッション累計報酬タイプ")]
	public enum MissionActivityRewardType
	{
		None,
		[Description("累計貢献メダル報酬")]
		TotalActivityMedal,
		[Description("ミッションクリア個数報酬")]
		TotalClearMission
	}
}
