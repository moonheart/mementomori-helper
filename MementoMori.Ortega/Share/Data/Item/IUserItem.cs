using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[Description("全てのアイテムが実装するインターフェース")]
	[Union(0, typeof(UserItem))]
	public interface IUserItem
	{
		[Description("アイテムの数")]
		[PropertyOrder(3)]
		long ItemCount
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("アイテムのID")]
		long ItemId
		{
			get;
		}

		[Description("アイテムの種類")]
		[PropertyOrder(1)]
		ItemType ItemType
		{
			get;
		}
	}
}
