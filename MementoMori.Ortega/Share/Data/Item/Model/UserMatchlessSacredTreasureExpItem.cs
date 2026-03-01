using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
	public class UserMatchlessSacredTreasureExpItem : IUserItem
	{
		public UserMatchlessSacredTreasureExpItem(MatchlessSacredTreasureExpItemType type, long itemCount)
		{
			this.ItemId = (long)type;
			this.ItemCount = itemCount;
		}

		public long ItemCount { get; }

		public long ItemId { get; }

		public ItemType ItemType { get; } = (ItemType)((ulong)15L);

		public int GetMatchlessSacredTreasureExp()
		{
			return this.ItemId == 2L
				? OrtegaConst.Item.MatchlessSacredTreasureExpItem2Count
				: OrtegaConst.Item.MatchlessSacredTreasureExpItem1Count;
		}
	}
}
