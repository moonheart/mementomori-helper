using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserLuckyChanceGachaTicket : IUserItem
	{
		public UserLuckyChanceGachaTicket(long itemId, long itemCount)
		{
			this.ItemId = itemId;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)40L);
	}
}
