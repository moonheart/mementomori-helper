using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ユーザー設定データ")]
	public enum PlayerSettingsType
	{
		[Description("不明")]
		None,
		[Description("レアリティNのキャラ自動販売")]
		AutoSellRarityNCharacter,
		[Description("週間トピックスのバトルリーグ掲載許可")]
		WeeklyTopicsBattleLeaguePostingPermission,
		[Description("週間トピックスのレジェンドリーグ掲載許可")]
		WeeklyTopicsLegendLeaguePostingPermission,
		[Description("ギルドバトルパーティ保護")]
		GuildBattlePartyMemberProtect
	}
}
