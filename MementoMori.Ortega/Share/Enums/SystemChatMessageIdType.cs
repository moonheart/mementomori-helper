using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("システムチャット")]
	public enum SystemChatMessageIdType
	{
		[Description("AギルドがBギルドのXX拠点に開戦を宣言しました！")]
		Declare = 1,
		[Description("AギルドがBギルドのXX拠点を占領しました！")]
		Occupy,
		[Description("AギルドがBギルドのXX拠点に奪還を宣言しました！")]
		Recapture,
		[Description("Ａがパーティを進軍させＢギルドのＸＸ城に攻撃を開始しました！")]
		Campaign,
		[Description("ＢギルドのＸＸ城に進軍させたパーティが全敗になった！加勢に向かいましょう！")]
		CompleteDefeat,
		[Description("占領しているXX城がＡギルドのＢに攻撃されました！急いで防衛に向かいましょう！")]
		Defense,
		[Description("今回AギルドによるXX拠点への攻撃の防衛に成功しました！")]
		Offense,
		[Description("レジェンドリーグ第１位\u3000[PlayerName]\nレジェンドリーグ第２位\u3000[PlayerName]\nレジェンドリーグ第３位\u3000[PlayerName]")]
		LegendLeague,
		[Description("バトルリーグ第１位\u3000[PlayerName]\nバトルリーグ第２位\u3000[PlayerName]\nバトルリーグ第３位\u3000[PlayerName]")]
		BattleLeague,
		[Description("マスターが[PlayerName]のギルド参加を承認しました")]
		GuildJoinApproved,
		[Description("新しく[PlayerName]がギルドに加入しました")]
		GuildJoinFree,
		[Description("[PlayerName]がギルドを脱退しました")]
		GuildLeave,
		[Description("マスターが[PlayerName]をギルドから追放しました")]
		GuildMemberRemove,
		[Description("[BeforePlayerName]が[AfterPlayerName]にプレイヤー名を変更しました")]
		ChangePlayerName,
		[Description("[プレイヤー名]がギルドツリーの第[数値]階層を突破しました！")]
		GuildTowerClearSpecialFloor,
		[Description("ギルドツリーでコンボが開始されました！\nギルドツリーに挑戦してコンボ数を増やそう！")]
		GuildTowerStartCombo,
		[Description("ギルドツリーで{プレイヤー名}の活躍で{数値}コンボを達成しました！")]
		GuildTowerAchieveCombo,
		[Description("Aギルドの[布告したプレイヤー名]がBギルドの[拠点名]に布告しました！")]
		DeclareToTargetGuild,
		[Description("Aギルドの[布告したプレイヤー名]がBギルドの[拠点名]に反撃を宣言しました！")]
		RecaptureToTargetGuild
	}
}
