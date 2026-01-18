using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ChatShop
{
	[MessagePackObject(true)]
	public class ChatShopItem
	{
		[Description("ChatShopMBのId")]
		public long ChatShopId { get; set; }

		[Description("獲得アイテム")]
		public UserItem GiveItem { get; set; }

		[Description("消費アイテム")]
		public UserItem ConsumeItem { get; set; }

		[Description("並び順")]
		public int SortOrder { get; set; }
	}
}
