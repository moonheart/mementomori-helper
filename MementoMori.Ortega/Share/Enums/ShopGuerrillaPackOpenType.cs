using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ゲリラパック解放タイプ")]
	public enum ShopGuerrillaPackOpenType
	{
		[Description("メインステージ")]
		AutoBattle = 1,
		[Description("プレイヤーランク")]
		PlayerRank,
		[Description("無窮の塔")]
		TowerBattle,
		[Description("藍の塔")]
		TowerBattleBlue,
		[Description("紅の塔")]
		TowerBattleRed,
		[Description("翠の塔")]
		TowerBattleGreen,
		[Description("黄の塔")]
		TowerBattleYellow,
		[Description("属性の塔の最低クリア階層")]
		TowerBattleMinClearElementTower
	}
}
