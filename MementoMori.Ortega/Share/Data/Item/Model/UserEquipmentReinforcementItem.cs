using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserEquipmentReinforcementItem : IUserItem
	{
		public UserEquipmentReinforcementItem(EquipmentReinforcementItemType itemType, long itemCount)
		{
			this.ItemId = (long)itemType;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)12L);
	}
}
