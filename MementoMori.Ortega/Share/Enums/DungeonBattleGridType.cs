using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("時空の洞窟 マス種別")]
	public enum DungeonBattleGridType
	{
		[Description("初期地点")]
		Start,
		[Description("バトルマス（通常）")]
		BattleNormal,
		[Description("バトルマス（エリート）")]
		BattleElite,
		[Description("バトルマス（ボス）")]
		BattleBoss,
		[Description("バトルマス（ボス）加護無し")]
		BattleBossNoRelic,
		[Description("回復マス")]
		Recovery,
		[Description("キャラ加入マス")]
		JoinCharacter,
		[Description("ミステリーショップマス")]
		Shop,
		[Description("加護強化マス")]
		RelicReinforce,
		[Description("加護の挑戦マス")]
		BattleAndRelicReinforce,
		[Description("カロンマス")]
		TreasureChest,
		[Description("復活")]
		Revival,
		[Description("イベント通常バトルマス")]
		EventBattleNormal,
		[Description("イベント強敵バトルマス")]
		EventBattleElite,
		[Description("イベント特殊バトルマス")]
		EventBattleSpecial
	}
}
