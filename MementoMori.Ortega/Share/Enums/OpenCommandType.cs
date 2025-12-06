using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("解放されるコマンドの種類")]
	public enum OpenCommandType
	{
		[Description("コマンド無し")]
		None,
		[Description("アルカナ")]
		Arcana,
		[Description("ショップ")]
		Shop,
		[Description("ミッション")]
		Mission,
		[Description("時空の洞窟")]
		DungeonBattle,
		[Description("バトルスキップ")]
		BattleSkip,
		[Description("バトル倍速")]
		BattleSpeed,
		[Description("バトル4倍速")]
		BattleSpeed4,
		[Description("時空の洞窟 見逃し補填")]
		DungeonBattleCompensation,
		[Description("無窮の塔")]
		TowerBattle,
		[Description("祈りの泉")]
		BountyQuest,
		[Description("バトルリーグ")]
		BattleLeague,
		[Description("幻影の神殿")]
		LocalRaid,
		[Description("レジェンドリーグ")]
		LegendLeague,
		[Description("運命ガチャ")]
		GachaDestiny,
		[Description("エスペリアタワー")]
		EsperiaTower,
		[Description("洞窟バトルハードモード")]
		DungeonBattleHardMode,
		[Description("メモリー")]
		CharacterStory,
		[Description("ロイヤルショップ")]
		RoyalShop,
		[Description("ランキング")]
		Ranking,
		[Description("スフィア装着")]
		SphereSet,
		[Description("武具合成通常")]
		ShopNormalEquipment,
		[Description("武具合成聖装")]
		ShopLegendEquipment,
		[Description("ギルド（GVG）")]
		ShopGuild,
		[Description("キャラコインショップ")]
		ShopCharacter,
		[Description("時空の洞窟")]
		ShopDungeonBattle,
		[Description("バトルリーグ")]
		ShopBattleLeague,
		[Description("レジェンドリーグ")]
		ShopLegendLeague,
		[Description("限定特典")]
		ShopLimited,
		[Description("進化")]
		RankUp,
		[Description("プレゼント")]
		Present,
		[Description("武具強化")]
		EquipmentStrength,
		[Description("神装強化")]
		EquipmentAscend,
		[Description("武具進化")]
		EquipmentEvolution,
		[Description("祈りの泉一括派遣、受け取り")]
		BountyQuestQuick,
		[Description("ギルドレイドの掃討")]
		GuildRaidQuick,
		[Description("ボス/無窮の塔掃討")]
		BossBattleQuick,
		[Description("武具ガチャ")]
		GachaEquipment,
		[Description("武具研磨")]
		EquipmentRefine,
		[Description("フレンド")]
		Friend,
		[Description("マイページ")]
		FooterMyPage,
		[Description("キャラ")]
		FooterCharacter,
		[Description("アイテムボックス")]
		FooterItemBox,
		[Description("試練")]
		FooterCompetition,
		[Description("ガチャ")]
		FooterGacha,
		[Description("チャット")]
		FooterChat,
		[Description("ギルド")]
		FooterGuild,
		[Description("魔水晶（ショップ）&キャラ詳細_専用武器交換")]
		ShopMagicCrystal,
		[Description("聖遺物（ショップ）")]
		ShopRelics,
		[Description("スフィア（ショップ）")]
		ShopSphere,
		[Description("イベント（ショップ）")]
		ShopEventExchange,
		[Description("グランドバトル（ショップ）")]
		ShopGrandBattle,
		[Description("時空の洞窟スキップ")]
		DungeonBattleSkip,
		[Description("武具一括研磨")]
		EquipmentBulkRefine,
		[Description("レベルリンク")]
		LevelLink = 80,
		[Description("チュートリアルスキップ")]
		SkipTutorial = 100,
		[Description("月間ログインボーナス")]
		MonthlyLoginBonus = 120,
		[Description("期間限定ログインボーナス")]
		LimitedLoginBonus,
		[Description("第二大陸")]
		SecondContinent = 140,
		[Description("星の導きガチャ")]
		GachaStarsGuidance = 160,
		[Description("楽曲再生")]
		MusicPlayer = 180,
		[Description("フレンドコード")]
		FriendCode = 200,
		[Description("ランキング到達報酬")]
		AchieveRanking = 220,
		[Description("貢献メダル達成報酬の一括受け取り説明テキスト")]
		ReceiveAllActivityMedalRewardText = 240,
		[Description("貢献メダル達成報酬の一括受け取り")]
		ReceiveAllActivityMedalReward,
		[Description("ゴールド交換")]
		GoldExchange = 260,
		[Description("星導交換所")]
		StarsGuidanceTradeShop = 280,
		[Description("神装強化(一括選択機能)")]
		BulkEquipmentAscend = 300,
		[Description("週間トピックス")]
		WeeklyTopics = 320,
		[Description("ラッキーチャンス")]
		LuckyChance = 340,
		[Description("人気投票")]
		PopularityVote = 360,
		[Description("武具一括進化")]
		BulkEquipmentEvolution = 380,
		[Description("進化解放")]
		RankRelease = 400,
		[Description("一括進化の優先設定")]
		RankUpPrioritySetting = 420,
		[Description("スフィア装着(一括装着カスタム)")]
		BulkSphereSet = 440,
		[Description("イベントポータル")]
		EventPortal = 460,
		[Description("アイテムボックス 消費タブ一括使用")]
		BulkUseItem = 480,
		[Description("模擬戦")]
		FriendBattle = 490,
		[Description("書庫整理")]
		BookSort = 500,
		[Description("武具Lvを超える強化Lv継承")]
		EquipmentOverReinforceLevelInheritance = 520,
		[Description("選択継承")]
		EquipmentSelectInheritance = 530,
		[Description("武具比較")]
		EquipmentComparison = 540,
		[Description("レンタルレイド")]
		RentalRaid = 560,
		[Description("武器シンクロ")]
		EquipmentSynchro = 570,
		[Description("武具リセット")]
		EquipmentReset = 580,
        [Description("セレクト音楽設定")]
        SelectMusicSetting = 600,
		[Description("武具固定")]
		LockEquipment = 1000,
		[Description("武具固定(ギルドバトル用)")]
		LockEquipmentGuildBattle,
		[Description("武具固定(属性の塔用)")]
		LockEquipmentElementTower
	}
}
