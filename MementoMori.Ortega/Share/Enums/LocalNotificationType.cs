using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ローカル通知種別")]
	public enum LocalNotificationType
	{
		None,
		[Description("オートバトル報酬上限到達")]
		AutoBattle,
		[Description("幻影の神殿開始")]
		LocalRaid,
		[Description("バトルリーグ報酬受け取り")]
		BattleLeagueReward,
		[Description("一週間パック終了")]
		OneWeekLimitedPack,
		[Description("GvG布告時間")]
		GvGDeclaration,
		[Description("GvG戦闘時間")]
		GvGBattle
	}
}
