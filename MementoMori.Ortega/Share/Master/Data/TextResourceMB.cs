using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("テキストリソース")]
	[MessagePackObject(true)]
	public class TextResourceMB : MasterBookBase
	{
		[PropertyOrder(2)]
		[Description("英語")]
		public string enUS
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("日本語")]
		public string jaJP
		{
			get;
		}

		[Description("韓国語")]
		[PropertyOrder(4)]
		public string koKR
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("文字列キー")]
		public string StringKey
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("中国語(繁体字)")]
		public string zhTW
		{
			get;
		}

		[SerializationConstructor]
		public TextResourceMB(long id, bool? isIgnore, string memo, string stringKey, string enUS, string jaJP, string koKR, string zhTW)
			:base(id, isIgnore, memo)
		{
			StringKey = stringKey;
			this.enUS = enUS;
			this.jaJP = jaJP;
			this.koKR = koKR;
			this.zhTW = zhTW;
		}

		public TextResourceMB() :base(0L, false, ""){}
	}
}
