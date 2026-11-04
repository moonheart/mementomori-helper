using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSort/unlockGridCell", true, false)]
	public class BookSortUnlockGridCellRequest : ApiRequestBase
	{
		public long GridCellUnlockItemId { get; set; }

		public int GridCellIndex { get; set; }
	}
}
