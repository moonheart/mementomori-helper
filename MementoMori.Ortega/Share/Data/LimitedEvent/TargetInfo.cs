using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LimitedEvent
{
	[MessagePackObject(true)]
	public class TargetInfo
	{
		[Description("値1")]
		public long Value1 { get; set; }

		[Description("値2")]
		public long Value2 { get; set; }
	}
}
