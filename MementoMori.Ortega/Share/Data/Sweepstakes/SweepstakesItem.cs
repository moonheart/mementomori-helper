using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Sweepstakes
{
	[MessagePackObject(true)]
	[Description("懸賞応募対象アイテム")]
	public class SweepstakesItem
	{
		[PropertyOrder(1)]
		[Description("アイテムの種類")]
		public ItemType ItemType { get; set; }

		[PropertyOrder(2)]
		[Description("アイテムのID")]
		public long ItemId { get; set; }
	}
}
