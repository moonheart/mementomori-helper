using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSort/selectBonusFloorReward", true, false)]
	public class BookSortSelectBonusFloorRewardRequest : ApiRequestBase
	{
		public int BonusFloorRewardIndex { get; set; }
	}
}
