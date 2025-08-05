using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("プレゼント配布方法")]
	public enum PresentType
	{
		[Description("不明")]
		None,
		[Description("バトルリーグ デイリー報酬")]
		BattleLeagueDaily,
		[Description("レジェンドリーグ デイリー報酬")]
		LegendLeagueDaily,
		[Description("レジェンドリーグ シーズン報酬")]
		LegendLeaguesSeason,
		[Description("グランドバトル シーズン報酬")]
		GrandBattleSeason,
		[Description("ギルドレイド 通常")]
		GuildRaidNormal,
		[Description("ギルドレイド 解放")]
		GuildRaidRelease,
		[Description("所持数上限を超えたアイテム")]
		OverLimitCount,
		[Description("運営からのプレゼント")]
		FromManagement,
		[Description("補填")]
		Compensation,
		[Description("月間ブースト 未受け取り報酬")]
		MonthlyBoostNotReceived,
		[Description("ダイヤ購入特典 未受取報酬")]
		DiamondPurchaseBonusNotReceived,
		[Description("課金付きミッション 未受け取り報酬")]
		CurrencyMissionNotReceived,
		[Description("VIPレベル到達報酬")]
		ReachVipLevelReward,
		[Description("累計ダイヤ購入特典 未受け取り報酬")]
		TotalDiamondPurchaseBonusNotReceived,
		[Description("毎日ダイヤ購入特典 未受け取り報酬")]
		DailyDiamondPurchaseBonus,
		[Description("アルカナ解放報酬")]
		CharacterCollectionOpenLevel,
		[Description("ギルドレイド イベント")]
		GuildRaidEvent,
		[Description("シリアルコード付与アイテム")]
		SerialCode,
		[Description("毎日ダイヤ購入特典(ギルド) ギルドメンバーへの配布")]
		DailyDiamondPurchaseBonusForGuildMember,
		[Description("毎日ダイヤ購入特典(ギルド) 未受け取り報酬")]
		DailyDiamondPurchaseBonusGuildNotReceived,
		[Description("アンケート報酬")]
		SurveyReward,
		[Description("ラッキーチャンス")]
		LuckyChance,
		[Description("レンタルレイド ランキング報酬")]
		RentalRaidRankingReward
	}
}
