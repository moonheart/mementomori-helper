using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ミッション達成条件タイプ")]
	public enum MissionAchievementType
	{
		[Description("無し")]
		None,
		[Description("日付をまたいでログインした時")]
		Login = 100,
		[Description("ダイヤによる購入")]
		BoughtByCurrency = 200,
		[Description("フレンドコード使用")]
		UseFriendCode = 300,
		[Description("新キャラミッション")]
		NewCharacter = 1000,
		[Description("カムバックミッション中に貢献メダルを獲得した時")]
		MissionTotalActivityAtComeback = 1010100,
		[Description("新キャラミッション中に貢献メダルを獲得した時")]
		MissionTotalActivityAtNewCharacterMission = 1010200,
		[Description("期間限定ミッション中に貢献メダルを獲得した時")]
		MissionTotalActivityAtEvent = 1010300,
		[Description("マイページで自己紹介文を変更した時")]
		PlayerInfoEditComment = 2010100,
		[Description("フレンドになった最大の人数")]
		FriendMaxFriendCount = 3010100,
		[Description("フレンドポイントを送信した時")]
		FriendSendFriendPointCount = 3010200,
		[Description("アカウント連携を行った時")]
		SocialAuthAccount = 4010100,
		[Description("公式Twitterフォロー")]
		SocialFollowOfficialTwitter = 4020100,
		[Description("公式Youtubeフォロー")]
		SocialFollowOfficialYoutube = 4020200,
		[Description("ショップ（聖装鋼タブ）購入回数")]
		ExchangeLegendForgeMergeCount = 5010100,
		[Description("ショップ（精錬鋼タブ）購入回数")]
		ExchangeEquipmentForgeMergeCount = 5020200,
		[Description("ショップ（全てのタブ）購入回数")]
		ExchangeAllBuyCount = 5030100,
		[Description("ショップ（レギュラータブ）購入回数")]
		ExchangeRegularBuyCount = 5030200,
		[Description("ショップ（ギルドタブ）購入回数")]
		ExchangeGvGBuyCount = 5040100,
		[Description("ショップ（時空の洞窟タブ）購入回数")]
		ExchangeDungeonBattleBuyCount = 5050100,
		[Description("ロイヤルショップのダイヤ購入でダイヤを購入した時")]
		ShopTotalBuyCurrency = 6010100,
		[Description("キャラレベルアップ")]
		CharacterLevelUpCount = 7010100,
		[Description("レベルリンク達成レベル")]
		CharacterLevelLinkMaxLevel = 7010200,
		[Description("武具達成レベル")]
		CharacterEquipmentMaxLevel = 7010300,
		[Description("Lv1スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel1 = 7010401,
		[Description("Lv2スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel2,
		[Description("Lv3スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel3,
		[Description("Lv4スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel4,
		[Description("Lv5スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel5,
		[Description("Lv6スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel6,
		[Description("Lv7スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel7,
		[Description("Lv8スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel8,
		[Description("Lv9スフィア装着個数")]
		CharacterSphereMaxEquipCountLevel9,
		[Description("魔装達成レベル")]
		CharacterMatchlessSacredTreasureMaxLevel = 7010500,
		[Description("聖装達成レベル")]
		CharacterLegendSacredTreasureMaxLevel = 7010600,
		[Description("武具研磨回数")]
		CharacterEquipmentTrainingCount = 7010700,
		[Description("武具強化達成レベル")]
		CharacterEquipmentReinforceMaxLevel = 7010800,
		[Description("神装強化回数")]
		CharacterEquipmentMergeCount = 7010900,
		[Description("最大総戦闘力")]
		CharacterMaxBattlePower = 7011000,
		[Description("キャラクター達成レベル")]
		CharacterCharacterMaxLevel = 7011100,
		[Description("武具強化最大達成レベル")]
		CharacterAllEquipmentReinforceMaxLevel = 7011200,
		[Description("キャラクター最高到達レアリティ")]
		CharacterRankUpMaxRarity = 7020100,
		[Description("キャラクター進化回数")]
		CharacterRankUpEvolutionCount = 7020200,
		[Description("レベルリンク枠解放数")]
		CharacterLevelLinkOpenSlotCount = 7030100,
		[Description("最高所持スフィアレベル")]
		EquipmentSphereMaxLevel = 8010100,
		[Description("スフィア合成回数")]
		EquipmentSphereComposeCount = 8010200,
		[Description("精錬武具（武具鋳造）個数")]
		EquipmentForgeCount = 8020100,
		[Description("Rナヘマー武具シリーズ合成回数")]
		EquipmentComposeCountR = 8030101,
		[Description("SRサンダルフォン武具シリーズ合成回数")]
		EquipmentComposeCountSR,
		[Description("SSRアスタロト武具シリーズ合成回数")]
		EquipmentComposeCountSSR,
		[Description("プレイヤー達成レベル")]
		AutoBattleMaxPlayerLevel = 9010100,
		[Description("獲得した合計領民数")]
		AutoBattleAddPopulation = 9010200,
		[Description("ボス勝利回数")]
		BossBattleVictoryCount = 9010300,
		[Description("最高到達クエスト")]
		AutoBattleMaxClearQuest = 9010400,
		[Description("最高到達章")]
		AutoBattledMaxClearChapter = 9010500,
		[Description("放置バトル報酬受け取り回数")]
		AutoBattleGetRewardCount = 9010600,
		[Description("高速バトル回数")]
		AutoBattleQuickCount = 9020100,
		[Description("時空の洞窟階層3クリア回数")]
		DungeonBattleClearThirdFloorCount = 10010100,
		[Description("時空の洞窟階層1クリア回数")]
		DungeonBattleClearFirstFloorCount = 10010200,
		[Description("時空の洞窟で〇人以上の○○タイプのキャラを使って戦闘に勝利")]
		DungeonBattleClearUnitJobTypeBase = 10010300,
		[Description("時空の洞窟で1人以上のウォリアータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear1UnitWarriorType = 10010311,
		[Description("時空の洞窟で1人以上のスナイパータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear1UnitSniperType,
		[Description("時空の洞窟で1人以上のソーサラータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear1UnitSorcererType = 10010314,
		[Description("時空の洞窟で2人以上のウォリアータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear2UnitWarriorType = 10010321,
		[Description("時空の洞窟で2人以上のスナイパータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear2UnitSniperType,
		[Description("時空の洞窟で2人以上のソーサラータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear2UnitSorcererType = 10010324,
		[Description("時空の洞窟で3人以上のウォリアータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear3UnitWarriorType = 10010331,
		[Description("時空の洞窟で3人以上のスナイパータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear3UnitSniperType,
		[Description("時空の洞窟で3人以上のソーサラータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear3UnitSorcererType = 10010334,
		[Description("時空の洞窟で4人以上のウォリアータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear4UnitWarriorType = 10010341,
		[Description("時空の洞窟で4人以上のスナイパータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear4UnitSniperType,
		[Description("時空の洞窟で4人以上のソーサラータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear4UnitSorcererType = 10010344,
		[Description("時空の洞窟で5人以上のウォリアータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear5UnitWarriorType = 10010351,
		[Description("時空の洞窟で5人以上のスナイパータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear5UnitSniperType,
		[Description("時空の洞窟で5人以上のソーサラータイプのキャラを使って戦闘に勝利")]
		DungeonBattleClear5UnitSorcererType = 10010354,
		[Description("時空の洞窟新キャラミッション")]
		DungeonBattleNewCharacter = 10011000,
		[Description("無窮の塔階層クリア数")]
		TowerBattleMaxClearFloor = 11010100,
		[Description("属性の塔到達下限階層")]
		TowerBattleMinClearElementTower = 11010200,
		[Description("無窮の塔勝利回数")]
		TowerBattleTotalWinCount = 11010300,
		[Description("バトルリーグ挑戦回数")]
		BattleLeagueChallengeCount = 12010100,
		[Description("バトルリーグ最高順位")]
		BattleLeagueMaxRanking = 12010200,
		[Description("幻影の神殿勝利回数")]
		LocalRaidVictoryCount = 13010100,
		[Description("祈りの泉クエスト受領回数")]
		BountyQuestAllStartQuestCount = 14010100,
		[Description("祈りの泉新キャラミッション")]
		BountyQuestNewCharacter = 14011000,
		[Description("祈りの泉マルチクエスト受領回数")]
		BountyQuestTeamStartQuestCount = 14020100,
		[Description("キャラ入手数")]
		GachaNewJoinCharacter = 15010100,
		[Description("キャラガチャ回数")]
		GachaDrawCount = 15010200,
		[Description("ダイヤ消費量")]
		ConsumeCurrencyCount = 15010300,
		[Description("ギルド加入回数")]
		GuildJoinCount = 16010100,
		[Description("ギルドログイン回数")]
		GuildLoginCount = 16010200,
		[Description("ギルドレイド回数（解放ボス含む）")]
		GuildGuildRaidChallengeCount = 16020100,
		[Description("ワールドチャット発言回数")]
		ChatSayWorldChatCount = 17010100,
		[Description("アップデート回数")]
		OsStoreUpdateCount = 18010100
	}
}
