using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Sweepstakes
{
	[MessagePackObject(true)]
	[Description("懸賞応募データ")]
	public class SweepstakesEntryData
	{
		[PropertyOrder(1)]
		[Description("応募アイテムのインデックス")]
		public int SweepstakesItemIndex { get; set; }

		[PropertyOrder(2)]
		[Description("応募口数")]
		public int EntryCount { get; set; }
	}
}
