using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class BookSortFloorHistory
	{
		public int Floor { get; set; }

		public int SelectedBonusFloorRewardIndex { get; set; }
	}
}
