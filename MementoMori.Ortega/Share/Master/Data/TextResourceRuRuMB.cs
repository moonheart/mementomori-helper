using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Master.Interfaces;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("ロシア語")]
	public class TextResourceRuRuMB : MasterBookBase, ITextResource
	{
		[Description("文字列キー")]
		[PropertyOrder(1)]
		public string StringKey { get; }

		[PropertyOrder(2)]
		[Description(null)]
		public string Text { get; }

		[SerializationConstructor]
		public TextResourceRuRuMB(long id, bool? isIgnore, string memo, string stringKey, string text) : base(id, isIgnore, memo)
		{
			this.StringKey = stringKey;
			this.Text = text;
		}

		public TextResourceRuRuMB() : base(0L, false, "")
		{
		}
	}
}
