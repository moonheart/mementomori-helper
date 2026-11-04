using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserCharacterFragment : IUserItem
	{
		public UserCharacterFragment(long characterId, long itemCount)
		{
			this.ItemId = characterId;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)7L);
	}
}
