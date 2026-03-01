using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserExchangePlaceItem : IUserItem
	{
		public UserExchangePlaceItem(ExchangePlaceItemType type, long itemCount)
		{
			this.ItemId = (long)type;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)13L);
	}
}
