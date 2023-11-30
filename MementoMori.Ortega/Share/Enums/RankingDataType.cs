using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ランキング種別")]
	public enum RankingDataType
	{
		[Description("不明")]
		None,
		[Description("プレイヤー戦闘力")]
		PlayerBattlePower,
		[Description("プレイヤーランク")]
		PlayerRank,
		[Description("プレイヤーメインクエスト")]
		PlayerMainQuest,
		[Description("プレイヤー無窮の塔")]
		PlayerTower,
		[Description("属性の塔 蒼")]
		TowerBlue,
		[Description("属性の塔 紅")]
		TowerRed,
		[Description("属性の塔 翠")]
		TowerGreen,
		[Description("属性の塔 黄")]
		TowerYellow
	}
}
