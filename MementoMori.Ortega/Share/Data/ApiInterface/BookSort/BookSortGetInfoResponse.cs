using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	public class BookSortGetInfoResponse : ApiResponseBase
	{
		public BookSortSyncData BookSortSyncData { get; set; }

		public bool IsUpdatedBonusFloorRewardLineup { get; set; }

		public bool IsExistAssistanceUpdate { get; set; }
	}
}
