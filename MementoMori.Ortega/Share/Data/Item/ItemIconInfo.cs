using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[MessagePackObject(true)]
	[Description("アイテムアイコン情報")]
	public class ItemIconInfo
	{
		[Description("アイテムID")]
		[PropertyOrder(1)]
		public long ItemId
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		[Description("アイテムタイプ")]
		public ItemType ItemType
		{
			get;
			set;
		}

		public ItemIconInfo()
		{
		}
	}
}
