using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Global
{
	[MessagePackObject(true)]
	[Description("各言語に翻訳されたテキスト")]
	public class TranslatedText
	{
		[Description("日本語")]
		[PropertyOrder(1)]
		public string jaJP { get; set; }

		[Description("英語")]
		[PropertyOrder(2)]
		public string enUS { get; set; }

		[PropertyOrder(3)]
		[Description("韓国語")]
		public string koKR { get; set; }

		[Description("中国語(繁体字)")]
		[PropertyOrder(4)]
		public string zhTW { get; set; }

		[Description("中国語(簡体字)")]
		[PropertyOrder(5)]
		public string zhCN { get; set; }

		[Description("フランス語")]
		[PropertyOrder(6)]
		public string frFR { get; set; }

		[PropertyOrder(7)]
		[Description("スペイン語")]
		public string esMX { get; set; }

		[Description("ポルトガル語")]
		[PropertyOrder(8)]
		public string ptBR { get; set; }

		[Description("タイ語")]
		[PropertyOrder(9)]
		public string thTH { get; set; }

		[PropertyOrder(10)]
		[Description("インドネシア語")]
		public string idID { get; set; }

		[PropertyOrder(11)]
		[Description("ベトナム語")]
		public string viVN { get; set; }
		[Description("ロシア語")]
		[PropertyOrder(12)]
		public string ruRU { get; set; }

		[Description("ドイツ語")]
		[PropertyOrder(13)]
		public string deDE { get; set; }
		[Description("アラビア語")]
		[PropertyOrder(14)]
		public string arEG { get; set; }

		public string GetText(LanguageType type)
		{
			switch (type)
			{
				case LanguageType.jaJP:
					return jaJP;
				case LanguageType.enUS:
					return enUS;
				case LanguageType.koKR:
					return koKR;
				case LanguageType.zhTW:
					return zhTW;
				case LanguageType.zhCN:
					return zhCN;
				case LanguageType.frFR:
					return frFR;
				case LanguageType.esMX:
					return esMX;
				case LanguageType.ptBR:
					return ptBR;
				case LanguageType.thTH:
					return thTH;
				case LanguageType.idID:
					return idID;
				case LanguageType.viVN:
					return viVN;
				case LanguageType.ruRU:
					return ruRU;
				case LanguageType.deDE:
					return deDE;
				default:
					return string.Empty;
			}
		}
	}
}
