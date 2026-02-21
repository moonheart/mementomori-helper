using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	public class BookSortUpFloorResponse : ApiResponseBase
	{
		public BookSortSyncData BookSortSyncData { get; set; }
	}
}
