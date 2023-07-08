using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Master.Interfaces;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("中国語(簡体字)")]
	public class TextResourceZhCnMB : MasterBookBase, ITextResource
	{
		[PropertyOrder(1)]
		[Description("文字列キー")]
		public string StringKey { get; }

		[PropertyOrder(2)]
		[Description(null)]
		public string Text { get; }

		[SerializationConstructor]
		public TextResourceZhCnMB(long id, bool? isIgnore, string memo, string stringKey, string text) : base(id, isIgnore, memo)
		{
			this.StringKey = stringKey;
			this.Text = text;
		}

		public TextResourceZhCnMB() : base(0L, false, "")
		{
		}
	}
}
