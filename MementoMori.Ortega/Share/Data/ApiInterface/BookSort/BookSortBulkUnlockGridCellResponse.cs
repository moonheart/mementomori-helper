using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	public class BookSortBulkUnlockGridCellResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UnlockGridCellResult> UnlockGridCellResultList { get; set; }

		public BookSortSyncData BookSortSyncData { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
