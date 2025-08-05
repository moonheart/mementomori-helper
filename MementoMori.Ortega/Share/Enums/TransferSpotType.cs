using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("遷移先タイプ")]
	public enum TransferSpotType
	{
		[Description("遷移無し")]
		None,
		[Description("放置バトル")]
		AutoBattle = 10,
		[Description("祈りの泉")]
		BountyQuest = 20,
		[Description("時空の洞窟")]
		DungeonBattle = 30,
		[Description("ガチャ")]
		GachaCase = 40,
		[Description("試練")]
		Competition = 50,
		[Description("幻影の神殿")]
		LocalRaid = 60,
		[Description("お知らせ")]
		Notice = 70,
		[Description("ロイヤルショップ（タブ指定）")]
		ShopTab = 80,
		[Description("ロイヤルショップ（商品指定）")]
		ShopItem,
		[Description("無窮の塔（通常）")]
		TowerBattle = 90,
		[Description("無窮の塔（塔指定）")]
		TowerBattleSelectTower,
		[Description("交換所")]
		TradeShop = 100,
		[Description("外部ウェブサイト")]
		OuterWebSite = 110,
		[Description("外部ウェブサイトwith確認ダイアログ")]
		OuterWebSiteWithDialog,
		[Description("月間ログインボーナス")]
		MonthlyLoginBonus = 120,
		[Description("期間限定ログインボーナス")]
		LimitedLoginBonus,
		[Description("初心者ミッション")]
		BeginnerMission = 130,
		[Description("カムバックミッション")]
		ComebackMission,
		[Description("新キャラミッション")]
		NewCharacterMission,
		[Description("イベントミッション")]
		EventMission,
		[Description("友達招待")]
		FriendCampaign,
		[Description("パネルミッション")]
		PanelMission,
		[Description("コラボミッション")]
		CollabMission,
		[Description("ゲリラパック")]
		GuerrillaPack = 140,
		[Description("格納アイコン")]
		StoreIcon = 150,
		[Description("キャラ")]
		Character = 160,
		[Description("チャット")]
		Chat = 170,
		[Description("ギルド")]
		Guild = 180,
		[Description("ギルドレイド")]
		GuildRaid,
		[Description("ギルドレイドワールドダメージ報酬ダイアログ")]
		GuildRaidWorldReward,
		[Description("アイテム自動回収ダイアログ")]
		RetrieveItem = 190,
		[Description("ギルド勧誘")]
		GuildMemberRecruit = 200,
		[Description("個別通知ダイアログ")]
		IndividualNotification = 210,
		[Description("個別通知_シリアルコード通知ダイアログ")]
		IndividualNotificationLiveTicketCode,
		[Description("星導交換所")]
		StarsGuidanceTradeShop = 220,
		[Description("初回インストール時のワールド指定")]
		TitleWorld = 230,
		[Description("ラッキーチャンス")]
		LuckyChance = 240,
		[Description("ラッキーチャンス当選者入力フォーム")]
		LuckyChanceInputForm = 250,
		[Description("週間トピックス")]
		WeeklyTopics = 260,
		[Description("人気投票")]
		PopularityVote = 270,
		[Description("イベントポータル")]
		EventPortal = 280,
		[Description("イベントポータル（サブ）")]
		SubEventPortal,
		[Description("書庫整理")]
		BookSort = 290,
		[Description("ワールド誘導")]
		WorldGuidance = 300,
		[Description("動画再生")]
		PlayVideo = 330,
		[Description("レンタルレイド")]
		RentalRaid = 340,
		[Description("フレンド")]
		Friend = 4
	}
}
