using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TradeShop
{
	[MessagePackObject(true)]
	public class ConsumeItemInfo
	{
		[Description("アイテムの種類")]
		public ItemType ItemType
		{
			get;
			set;
		}

		[Description("アイテムのID")]
		public long ItemId
		{
			get;
			set;
		}

		public ConsumeItemInfo()
		{
		}
	}
}
