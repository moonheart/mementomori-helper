using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Master.Interfaces;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ドイツ語")]
	[MessagePackObject(true)]
	public class TextResourceDeDeMB : MasterBookBase, ITextResource
	{
		[PropertyOrder(1)]
		[Description("文字列キー")]
		public string StringKey { get; }

		[Description(null)]
		[PropertyOrder(2)]
		public string Text { get; }

		[SerializationConstructor]
		public TextResourceDeDeMB(long id, bool? isIgnore, string memo, string stringKey, string text) : base(id, isIgnore, memo)
		{
			this.StringKey = stringKey;
			this.Text = text;
		}

		public TextResourceDeDeMB() : base(0L, false, "")
		{
		}
	}
}
