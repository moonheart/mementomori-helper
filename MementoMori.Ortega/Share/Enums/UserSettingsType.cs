using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("ユーザー設定タイプ")]
	public enum UserSettingsType
	{
		[Description("不明")]
		None,
		[Description("テキスト言語")]
		TextLanguage,
		[Description("ボイス言語")]
		VoiceLanguage,
		[Description("オプトイン/アウト")]
		PrivacySettings
	}
}
