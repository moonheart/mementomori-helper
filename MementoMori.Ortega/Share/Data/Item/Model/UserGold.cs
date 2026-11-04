using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserGold : IUserItem
	{
		public UserGold(long itemCount)
		{
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; } = (long)((ulong)1L);

		public ItemType ItemType { get; } = (ItemType)((ulong)3L);
	}
}
