using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("キャラクターボイスの分類")]
	public enum CharacterVoiceCategory
	{
		[Description("通常セリフ")]
		Basic,
		[Description("誕生日セリフ")]
		Birthday,
		[Description("カムバックセリフ")]
		ComeBack,
		[Description("ログインセリフ")]
		Login,
		[Description("ランクアップセリフ")]
		RankUp,
		[Description("その他")]
		Other,
		[Description("登場")]
		Appear,
		[Description("決め台詞")]
		SignaturePhrase,
		[Description("バトル勝利")]
		BattleWin,
		[Description("バトル敗北")]
		BattleLose,
		[Description("朗読")]
		Monologue
	}
}
