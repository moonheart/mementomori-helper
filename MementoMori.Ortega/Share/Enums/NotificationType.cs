using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("通知タイプ")]
	public enum NotificationType
	{
		None,
		[Description("新しいガチャ権を入手")]
		NewGachaTicket,
		[Description("セレクトリストに登録しているキャラのレアリティが最大")]
		MaxRarityInSelectList,
		[Description("セレクトリストに登録できるキャラが追加")]
		NewCharacterInSelectList,
		[Description("無料ガチャ回数が残っている")]
		GachaFreeCount,
		[Description("ギルドレイドコンテンツ挑戦/解放可能")]
		GuildRaidAvailable,
		[Description("新しいギルド加入申請")]
		NewGuildJoinRequest,
		[Description("ギルドレベルアップ")]
		GuildLevelUp,
		[Description("Local GVG 受け取り可能なギルドバトル報酬がある場合")]
		LocalGvgReward,
		[Description("Global GVG 受け取り可能なギルドバトル報酬がある場合")]
		GlobalGvgReward,
		[Description("新しく登録された回収アイテムがある場合")]
		NewRetrieveItem,
		[Description("ギルドミッションの受け取り可能な報酬がある場合")]
		ReceivableGuildMission,
		[Description("新しいギルドメンバー勧誘がある場合")]
		NewRecruitGuildMember,
		[Description("ギルドツリーミッションの受取可能な報酬がある場合")]
		ReceivableGuildTowerMission,
		[Description("人気投票のミッションで報酬を受け取れるものがあるとき")]
		ReceivablePopularityVoteMission,
		[Description("投票報酬で報酬を受け取れるものがあるとき")]
		ReceivablePopularityVoteReward,
		[Description("投票チケットを1枚以上もっているかつ、現在予選期間内で、その予選中に一度も投票したことが無い時")]
		NotPopularityVoteInPreliminary,
		[Description("投票チケットを1枚以上もっているかつ、現在本選期間内で、その本選中に一度も投票したことが無い時")]
		NotPopularityVoteInFinal,
		[Description("投票チケットを1枚以上もっているかつ、予選本選いずれかの期間内で、その日一度も投票してない時")]
		NotPopularityVoteOnDay,
		[Description("予選中間発表の結果が出た時（現在時間が予選中間発表日時を過ぎると表示）")]
		PreliminaryInterimResult,
		[Description("予選結果が出た時（現在時間が本選開始日時を過ぎると表示）")]
		PreliminaryResult,
		[Description("本選中間発表の結果が出た時（現在時間が本選中間発表日時を過ぎると表示）")]
		FinalInterimResult,
		[Description("本選結果が出た時（現在時間が結果発表開始日時を過ぎると表示）")]
		FinalResult,
		[Description("進化解放可能")]
		RankRelease,
		[Description("新規獲得したチャットふきだしがあるとき")]
		ChatBalloon,
		[Description("外部サイトへ遷移できるマイページアイコンをタップしていない場合")]
		MyPageIconWithOuterWebSite,
		[Description("外部サイトへ遷移できるイベントポータルをタップしていない場合")]
		EventPortalWithOuterWebSite,
		[Description("動画を一度も再生していない場合（マイページから遷移される動画）")]
		PlayVideoFromMyPage,
		[Description("動画を一度も再生していない場合（イベントポータルから遷移される動画）")]
		PlayVideoFromEventPortal,
		[Description("人気投票 グループ投票チケットを1枚以上もっているかつ、予選本選いずれかの期間内で、その日一度も投票してない時")]
		NotPopularityVoteGroupOnDay
	}
}
