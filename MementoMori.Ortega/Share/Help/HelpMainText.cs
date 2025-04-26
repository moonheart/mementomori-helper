using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Help
{
	[MessagePackObject(true)]
	[Description("ヘルプ本文")]
	public class HelpMainText
	{
		[PropertyOrder(1)]
		[Description("本文")]
		public string MainText { get; set; }
	}
}
