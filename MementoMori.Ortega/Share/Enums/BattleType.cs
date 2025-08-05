using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("バトルタイプ")]
	public enum BattleType
	{
		[Description("バトルタイプなし")]
		None,
		[Description("放置")]
		Auto,
		[Description("ボス戦")]
		Boss,
		[Description("ギルドバトル")]
		GuildBattle,
		[Description("グランドバトル")]
		GrandBattle,
		[Description("バトルリーグ")]
		BattleLeague,
		[Description("レジェンドリーグ")]
		LegendLeague,
		[Description("幻影の神殿")]
		LocalRaid,
		[Description("無窮の塔")]
		TowerBattle,
		[Description("時空の洞窟")]
		DungeonBattle,
		[Description("ギルドレイド")]
		GuildRaid = 11,
		[Description("ギルドツリー")]
		GuildTower,
		[Description("模擬戦")]
		FriendBattle,
		[Description("レンタルレイド通常バトル")]
		RentalRaidNormalBattle,
		[Description("レンタルレイドボスバトル")]
		RentalRaidBossBattle
	}
}
