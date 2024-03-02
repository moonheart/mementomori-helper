using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("遷移先")]
	public enum ViewTransitionType
	{
		[Description("キャラ育成")]
		CharacterTraining,
		[Description("キャラランクアップ")]
		CharacterRankUp,
		[Description("遷移なしの編成ダイアログ")]
		FormationWithoutTransition,
		[Description("放置バトルMAP")]
		Map,
		[Description("ミッション")]
		Mission,
		[Description("ギルドツリー強化ダイアログ")]
		GuildTowerReinforcement
	}
}
