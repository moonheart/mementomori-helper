using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum UnitType
	{
		[Description("キャラクター")]
		Character,
		[Description("放置バトルの敵")]
		AutoBattleEnemy,
		[Description("時空の洞窟の敵")]
		DungeonBattleEnemy,
		[Description("ギルドレイドボス")]
		GuildRaidBoss,
		[Description("ボスバトルの敵")]
		BossBattleEnemy,
		[Description("無窮の塔の敵")]
		TowerBattleEnemy,
		[Description("幻影の神殿の敵")]
		LocalRaidEnemy,
		[Description("ギルドツリーの敵")]
		GuildTowerEnemy,
		[Description("レンタルレイドの通常敵")]
		RentalRaidNormalEnemy,
		[Description("レンタルレイドのボス")]
		RentalRaidBossEnemy,
		[Description("協力キャラクター")]
		ShareCharacter
	}
}
