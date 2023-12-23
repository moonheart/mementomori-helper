using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class CustomTextLayout
	{
		[Description("日本語")]
		[Nest(true, 2)]
		[PropertyOrder(1)]
		public CustomTextLayoutInfo jaJP { get; set; }

		[Description("英語")]
		[PropertyOrder(2)]
		[Nest(true, 2)]
		public CustomTextLayoutInfo enUS { get; set; }

		[PropertyOrder(3)]
		[Description("韓国語")]
		[Nest(true, 2)]
		public CustomTextLayoutInfo koKR { get; set; }

		[Description("中国語(繁体字)")]
		[PropertyOrder(4)]
		[Nest(true, 2)]
		public CustomTextLayoutInfo zhTW { get; set; }

		[Nest(true, 2)]
		[Description("中国語(簡体字)")]
		[PropertyOrder(5)]
		public CustomTextLayoutInfo zhCN { get; set; }
		[PropertyOrder(6)]
		[Description("フランス語")]
		[Nest(true, 2)]
		public CustomTextLayoutInfo frFR { get; set; }

		[Description("スペイン語")]
		[PropertyOrder(7)]
		[Nest(true, 2)]
		public CustomTextLayoutInfo esMX { get; set; }

		[Description("ポルトガル語")]
		[Nest(true, 2)]
		[PropertyOrder(8)]
		public CustomTextLayoutInfo ptBR { get; set; }

		[PropertyOrder(9)]
		[Description("タイ語")]
		[Nest(true, 2)]
		public CustomTextLayoutInfo thTH { get; set; }

		[PropertyOrder(10)]
		[Description("インドネシア語")]
		[Nest(true, 2)]
		public CustomTextLayoutInfo idID { get; set; }

		[Description("ベトナム語")]
		[Nest(true, 2)]
		[PropertyOrder(11)]
		public CustomTextLayoutInfo viVN { get; set; }

		[PropertyOrder(12)]
		[Description("ロシア語")]
		[Nest(true, 2)]
		public CustomTextLayoutInfo ruRU { get; set; }

		[Description("ドイツ語")]
		[PropertyOrder(13)]
		[Nest(true, 2)]
		public CustomTextLayoutInfo deDE { get; set; }

		[Nest(true, 2)]
		[PropertyOrder(14)]
		[Description("アラビア語")]
		public CustomTextLayoutInfo arEG { get; set; }

		public CustomTextLayoutInfo GetCustomTextLayoutInfo(LanguageType type)
		{
			switch (type)
			{
				case LanguageType.deDE: return deDE;
				case LanguageType.enUS: return enUS;
				case LanguageType.esMX: return esMX;
				case LanguageType.frFR: return frFR;
				case LanguageType.idID: return idID;
				case LanguageType.jaJP: return jaJP;
				case LanguageType.koKR: return koKR;
				case LanguageType.ptBR: return ptBR;
				case LanguageType.ruRU: return ruRU;
				case LanguageType.thTH: return thTH;
				case LanguageType.viVN: return viVN;
				case LanguageType.zhCN: return zhCN;
				case LanguageType.zhTW: return zhTW;
				default: return jaJP;
			}
		}
	}
}
