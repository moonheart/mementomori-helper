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
		[Description("パネルミッション中に貢献メダルを獲得した時")]
		MissionTotalActivityAtPanelMission = 1010400,
		[Description("コラボミッション中に貢献メダルを獲得した時")]
		MissionTotalActivityAtCollabMission = 1010500,
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
		[Description("Url1遷移")]
		Url1Transition = 4020300,
		[Description("Url2遷移")]
		Url2Transition = 4020400,
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
		[Description("幻影の神殿で〇人以上の○属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictoryUnitElementTypeBase = 13010200,
		[Description("幻影の神殿で1人以上の天属性か冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory1UnitLightAndDarkType = 13010210,
		[Description("幻影の神殿で1人以上の藍属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory1UnitBlueType,
		[Description("幻影の神殿で1人以上の紅属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory1UnitRedType,
		[Description("幻影の神殿で1人以上の翠属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory1UnitGreenType,
		[Description("幻影の神殿で1人以上の黄属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory1UnitYellowType,
		[Description("幻影の神殿で1人以上の天属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory1UnitLightType,
		[Description("幻影の神殿で1人以上の冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory1UnitDarkType,
		[Description("幻影の神殿で2人以上の天属性か冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory2UnitLightAndDarkType = 13010220,
		[Description("幻影の神殿で2人以上の藍属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory2UnitBlueType,
		[Description("幻影の神殿で2人以上の紅属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory2UnitRedType,
		[Description("幻影の神殿で2人以上の翠属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory2UnitGreenType,
		[Description("幻影の神殿で2人以上の黄属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory2UnitYellowType,
		[Description("幻影の神殿で2人以上の天属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory2UnitLightType,
		[Description("幻影の神殿で2人以上の冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory2UnitDarkType,
		[Description("幻影の神殿で3人以上の天属性か冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory3UnitLightAndDarkType = 13010230,
		[Description("幻影の神殿で3人以上の藍属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory3UnitBlueType,
		[Description("幻影の神殿で3人以上の紅属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory3UnitRedType,
		[Description("幻影の神殿で3人以上の翠属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory3UnitGreenType,
		[Description("幻影の神殿で3人以上の黄属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory3UnitYellowType,
		[Description("幻影の神殿で3人以上の天属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory3UnitLightType,
		[Description("幻影の神殿で3人以上の冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory3UnitDarkType,
		[Description("幻影の神殿で4人以上の天属性か冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory4UnitLightAndDarkType = 13010240,
		[Description("幻影の神殿で4人以上の藍属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory4UnitBlueType,
		[Description("幻影の神殿で4人以上の紅属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory4UnitRedType,
		[Description("幻影の神殿で4人以上の翠属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory4UnitGreenType,
		[Description("幻影の神殿で4人以上の黄属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory4UnitYellowType,
		[Description("幻影の神殿で4人以上の天属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory4UnitLightType,
		[Description("幻影の神殿で4人以上の冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory4UnitDarkType,
		[Description("幻影の神殿で5人以上の天属性か冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory5UnitLightAndDarkType = 13010250,
		[Description("幻影の神殿で5人以上の藍属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory5UnitBlueType,
		[Description("幻影の神殿で5人以上の紅属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory5UnitRedType,
		[Description("幻影の神殿で5人以上の翠属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory5UnitGreenType,
		[Description("幻影の神殿で5人以上の黄属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory5UnitYellowType,
		[Description("幻影の神殿で5人以上の天属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory5UnitLightType,
		[Description("幻影の神殿で5人以上の冥属性のキャラで編成して戦闘に勝利（自身の編成のみ有効）")]
		LocalRaidVictory5UnitDarkType,
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
		[Description("ギルドチャット発言回数")]
		ChatSayGuildChatCount = 17010200,
		[Description("アップデート回数")]
		OsStoreUpdateCount = 18010100,
		[Description("パネル図鑑遷移")]
		PictureBookTransitionPanel = 21010100,
		[Description("楽曲再生画面遷移回数")]
		MusicPlayerTransitionCount = 22010100,
		[Description("指定楽曲開放")]
		BuyMusic = 22010200,
		[Description("楽曲プレイリスト共有")]
		ShareMusicPlaylist = 22010300,
		[Description("ギルドツリーで1つのタイプのキャラを〇体以上使って戦闘に勝利")]
		GuildTowerWinUnitSameJobTypeBase = 23010100,
		[Description("ギルドツリーで1つのタイプのキャラを1体以上使って戦闘に勝利")]
		GuildTowerWin1UnitSameJobType,
		[Description("ギルドツリーで1つのタイプのキャラを2体以上使って戦闘に勝利")]
		GuildTowerWin2UnitSameJobType,
		[Description("ギルドツリーで1つのタイプのキャラを3体以上使って戦闘に勝利")]
		GuildTowerWin3UnitSameJobType,
		[Description("ギルドツリーで1つのタイプのキャラを4体以上使って戦闘に勝利")]
		GuildTowerWin4UnitSameJobType,
		[Description("ギルドツリーで1つのタイプのキャラを5体以上使って戦闘に勝利")]
		GuildTowerWin5UnitSameJobType,
		[Description("ギルドツリーの勝利回数")]
		GuildTowerWinCount = 23010200,
		[Description("ギルドツリーでの最大達成コンボ数")]
		GuildTowerMaxComboCount = 23010300,
		[Description("開花の種の獲得数")]
		GuildTowerGetJobReinforcementMaterialCount = 23010400,
		[Description("全てのタイプのギルドツリー強化Lvの内で最大到達のレベル")]
		GuildTowerMaxJobLevel = 23020100,
		[Description("全てのタイプのギルドツリー強化Lvの内で最低到達のレベル")]
		GuildTowerMinJobLevel = 23020200,
		[Description("週間トピックス遷移")]
		WeeklyTopicsTransitionCount = 24010100,
		[Description("人気投票チケットの消費数")]
		ConsumePopularityVoteTicket = 25010100,
		[Description("グループ投票チケットの消費数")]
		ConsumeGroupVotingTicket = 25010200,
		[Description("魔女の書庫整理 マス解放数")]
		BookSortUnlockGridCell = 26010100,
		[Description("魔女の書庫整理 最大到達フロア")]
		BookSortMaxFloor = 26010200,
		[Description("魔女の書庫整理 アイテムの消費量")]
		BookSortConsumeItemCount = 26010300,
		[Description("動画再生機能の再生回数（全サーバー合算）")]
		PlayVideoTotalCount = 27010100,
		[Description("動画再生機能の再生回数（個人）")]
		PlayVideoCount = 27010200,
		[Description("レンタルレイド 前半レイドステージ制限編成の達成ダメージ")]
		RentalRaidFirstHalfLimitFormation = 28010100,
		[Description("レンタルレイド 後半レイドステージ制限編成の達成ダメージ")]
		RentalRaidSecondHalfLimitFormation = 28010200
	}
}
