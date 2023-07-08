using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Master.Interfaces;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ベトナム語")]
	public class TextResourceViVnMB : MasterBookBase, ITextResource
	{
		[Description("文字列キー")]
		[PropertyOrder(1)]
		public string StringKey { get; }

		[Description(null)]
		[PropertyOrder(2)]
		public string Text { get; }

		[SerializationConstructor]
		public TextResourceViVnMB(long id, bool? isIgnore, string memo, string stringKey, string text) : base(id, isIgnore, memo)
		{
			this.StringKey = stringKey;
			this.Text = text;
		}

		public TextResourceViVnMB() : base(0L, false, "")
		{
		}
	}
}
