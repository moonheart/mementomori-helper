using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserGuildExp : IUserItem
	{
		public UserGuildExp(long itemCount)
		{
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; } = (long)((ulong)1L);

		public ItemType ItemType { get; } = (ItemType)((ulong)27L);
	}
}
