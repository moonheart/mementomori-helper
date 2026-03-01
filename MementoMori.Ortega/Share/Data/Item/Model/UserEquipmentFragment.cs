using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserEquipmentFragment : IUserItem
	{
		public UserEquipmentFragment(long equipmentId, long itemCount)
		{
			this.ItemId = equipmentId;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)5L);
	}
}
