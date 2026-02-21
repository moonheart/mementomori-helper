using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSort/bulkUnlockGridCell", true, false)]
	public class BookSortBulkUnlockGridCellRequest : ApiRequestBase
	{
	}
}
