using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserCurrencyPaid : IUserItem
	{
		public UserCurrencyPaid(DeviceType deviceType, long itemCount)
		{
			this.ItemId = (long)deviceType;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)2L);
	}
}
