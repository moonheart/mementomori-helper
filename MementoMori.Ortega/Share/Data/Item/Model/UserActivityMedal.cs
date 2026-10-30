using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserActivityMedal : IUserItem
	{
		public UserActivityMedal(ActivityMedalType type, long itemCount)
		{
			this.ItemCount = itemCount;
			this.ItemId = (long)type;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)28L);
	}
}
