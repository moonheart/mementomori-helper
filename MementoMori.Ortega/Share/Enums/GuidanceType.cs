using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("誘導タイプ")]
	public enum GuidanceType
	{
		[Description("なし")]
		None,
		[Description("勧誘設定")]
		RecruitSetting,
		[Description("ギルド加入")]
		GuildJoining,
		[Description("勧誘設定")]
		RecruitSetting2,
		[Description("ワールド誘導")]
		WorldGuidance
	}
}
