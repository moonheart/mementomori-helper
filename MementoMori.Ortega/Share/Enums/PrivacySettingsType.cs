using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("プライバシー設定タイプ")]
	public enum PrivacySettingsType
	{
		[Description("未設定")]
		None,
		[Description("オプトイン")]
		OptIn,
		[Description("オプトアウト")]
		OptOut
	}
}
