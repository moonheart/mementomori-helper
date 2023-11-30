using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("PvPランキング報酬タイプ")]
	public enum PvpRankingRewardType
	{
		[Description("バトルリーグデイリーランキング報酬")]
		BattleLeagueDailyRankingReward,
		[Description("レジェンドリーグデイリーランキング報酬")]
		LegendLeagueDailyRankingReward,
		[Description("レジェンドリーグシーズンランキング報酬")]
		LegendLeagueSeasonRankingReward,
		[Description("レジェンドリーグアイコンランキング報酬")]
		LegendLeagueIconRankingReward
	}
}
