using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Sweepstakes
{
	[MessagePackObject(true)]
	[Description("懸賞応募状況")]
	public class SweepstakesEntryInfo
	{
		[PropertyOrder(1)]
		[Description("応募アイテム")]
		public SweepstakesItem SweepstakesItem { get; set; }

		[PropertyOrder(2)]
		[Description("応募回数")]
		public int EntryCount { get; set; }
	}
}
