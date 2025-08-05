using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ミッショングループ")]
	public enum MissionGroupType
	{
		[Description("メイン")]
		Main,
		[Description("デイリー")]
		Daily,
		[Description("ウィークリー")]
		Weekly,
		[Description("初心者")]
		Beginner,
		[Description("カムバック")]
		Comeback,
		[Description("新キャラ")]
		NewCharacter,
		[Description("イベント")]
		Limited,
		[Description("パネル")]
		Panel = 9,
		[Description("ギルドミッション")]
		Guild,
		[Description("ギルドツリー")]
		GuildTower,
		[Description("コラボ")]
		Collab,
		[Description("人気投票")]
		PopularityVote,
		[Description("魔女の書庫整理")]
		BookSort,
		[Description("レンタルレイド")]
		RentalRaid,
		[Description("デイリー追加報酬")]
		DailyBonus = 1000
	}
}
