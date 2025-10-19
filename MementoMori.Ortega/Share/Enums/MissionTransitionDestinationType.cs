using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ミッション遷移先タイプ")]
	public enum MissionTransitionDestinationType
	{
		[Description("遷移先無し")]
		None,
		[Description("デイリーミッション")]
		MissionDaily = 101,
		[Description("プレイヤー情報")]
		PlayerInfo = 201,
		[Description("フレンド一覧")]
		Friend = 301,
		[Description("アカウント連携")]
		LinkAccount = 401,
		[Description("Twitter ")]
		Twitter,
		[Description("YouTube")]
		YouTube,
		[Description("Url1")]
		Url1,
		[Description("Url2")]
		Url2,
		[Description("ショップ（武具合成\uff3f聖装タブ）")]
		ExchangeLegendForge = 501,
		[Description("ショップ（武具合成\uff3f通常タブ）")]
		ExchangeEquipmentForge,
		[Description("ショップ（店舗タブ）")]
		Exchange,
		[Description("GvGショップ")]
		ExchangeGvG,
		[Description("時空の洞窟ショップ")]
		ExchangeDungeonBattle,
		[Description("ロイヤルショップ\uff3fダイヤ購入タブ")]
		Shop = 601,
		[Description("キャラ画面（所持キャラタブ）")]
		CharacterList = 701,
		[Description("キャラ画面（進化タブ）")]
		CharacterRankUp,
		[Description("レベルリンク（共鳴クリスタル）")]
		LevelLink,
		[Description("アイテムボックス_スフィア")]
		ItemBoxSphere = 801,
		[Description("アイテムボックス_武具")]
		ItemBoxEquipment,
		[Description("アイテムボックス画面（パーツタブ）")]
		ItemBoxParts,
		[Description("オートバトル")]
		AutoBattle = 901,
		[Description("高速バトルダイアログ")]
		AutoBattleQuick,
		[Description("時空の洞窟")]
		DungeonBattle = 1001,
		[Description("無窮の塔")]
		TowerBattle = 1101,
		[Description("バトルリーグ")]
		BattleLeague = 1201,
		[Description("幻影の神殿")]
		LocalRaid = 1301,
		[Description("祈りの泉（ノーマルタブ）")]
		BountyQuestSolo = 1401,
		[Description("祈りの泉（チームタブ）")]
		BountyQuestTeam,
		[Description("ガチャ（キャラタブ）")]
		Gacha = 1501,
		[Description("ギルド")]
		Guild = 1601,
		[Description("ギルドレイド画面（ソーニャ）")]
		GuildRaid,
		[Description("チャット")]
		Chat = 1701,
		[Description("チャット（ギルドタブ）")]
		ChatGuild,
		[Description("各OSのストア")]
		OsStore = 1801,
		[Description("キャラ詳細")]
		CharacterDetail = 1901,
		[Description("マイページお気に入り設定ダイアログ")]
		FavoriteCharacter = 2001,
		[Description("パネル図鑑")]
		PanelPictureBook = 2101,
		[Description("楽曲再生")]
		MusicPlayer = 2201,
		[Description("楽曲プレイリスト")]
		MusicPlaylist,
		[Description("ギルドツリーメイン画面")]
		GuildTower = 2301,
		[Description("ギルドツリーLV強化ダイアログ")]
		GuildTowerReinforceJob,
		[Description("週間トピックス")]
		WeeklyTopics = 2401,
		[Description("人気投票メイン画面")]
		PopularityVote = 2501,
		[Description("魔女の書庫整理")]
		BookSort = 2601,
		[Description("動画再生")]
		PlayVideo = 2701,
		[Description("レンタルレイド")]
		RentalRaid = 2801
	}
}
