using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("バトルタイプ")]
	public enum BattleScheduleType
	{
		[Description("バトルタイプなし")]
		None,
		[Description("クエスト(ボス、オートバトル)")]
		Quest,
		[Description("無窮の塔")]
		TowerInfinite,
		[Description("愁（しゅう）")]
		TowerBlue,
		[Description("業（ごう）")]
		TowerRed,
		[Description("心（しん）")]
		TowerGreen,
		[Description("渇（かつ）")]
		TowerYellow
	}
}
