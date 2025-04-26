using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ミッションパスタイプ")]
	public enum MissionPassType
	{
		[Description("なし")]
		None,
		[Description("デイリーミッションとウィークリーミッションの貢献メダル")]
		DailyAndWeeklyMissionActivityMedal
	}
}
