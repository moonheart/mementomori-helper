using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("言語")]
	public enum LanguageType
	{
		None,
		[Description("日本語")]
		jaJP,
		[Description("英語")]
		enUS,
		[Description("韓国語")]
		koKR,
		[Description("中国語(繁体字)")]
		zhTW
	}
}
