using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Global
{
	[MessagePackObject(true)]
	[Description("各言語に翻訳されたテキスト")]
	public class TranslatedText
	{
		[PropertyOrder(2)]
		[Description("英語")]
		public string enUS { get; set; }

		[PropertyOrder(1)]
		[Description("日本語")]
		public string jaJP{ get; set; }

		[PropertyOrder(3)]
		[Description("韓国語")]
		public string koKR{ get; set; }

		[Description("中国語(繁体字)")]
		[PropertyOrder(4)]
		public string zhTW{ get; set; }

		public string GetText(LanguageType type)
		{
			switch (type)
			{
				case LanguageType.enUS:
					return enUS;
				case LanguageType.jaJP:
					return jaJP;
				case LanguageType.koKR:
					return koKR;
				case LanguageType.zhTW:
					return zhTW;
				default:
					return "";
			}
		}

		public TranslatedText()
		{
		}
	}
}
