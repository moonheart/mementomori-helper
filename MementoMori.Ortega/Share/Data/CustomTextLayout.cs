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
		[Description("英語")]
		[Nest(true, 2)]
		[PropertyOrder(2)]
		public CustomTextLayoutInfo enUS
		{
			get;
			set;
		}

		[Nest(true, 2)]
		[PropertyOrder(1)]
		[Description("日本語")]
		public CustomTextLayoutInfo jaJP
		{
			get;
			set;
		}

		[Nest(true, 2)]
		[PropertyOrder(3)]
		[Description("韓国語")]
		public CustomTextLayoutInfo koKR
		{
			get;
			set;
		}

		[Description("中国語(繁体字)")]
		[Nest(true, 2)]
		[PropertyOrder(5)]
		public CustomTextLayoutInfo zhTW
		{
			get;
			set;
		}

		public CustomTextLayoutInfo GetCustomTextLayoutInfo(LanguageType type)
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
				return null;
			}
		}

		public CustomTextLayout()
		{
		}
	}
}
