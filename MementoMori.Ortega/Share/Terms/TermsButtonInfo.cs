using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Terms
{
	[MessagePackObject(true)]
	[Description("規約ボタン情報")]
	public class TermsButtonInfo
	{
		[Description("ボタンテキストKey")]
		[PropertyOrder(1)]
		public string Name
		{
			get;
			set;
		}

		[Description("Url")]
		[PropertyOrder(2)]
		public string Url
		{
			get;
			set;
		}

		public TermsButtonInfo()
		{
		}
	}
}
