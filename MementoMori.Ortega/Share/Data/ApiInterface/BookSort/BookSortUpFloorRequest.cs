using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSort/upFloor", true, false)]
	public class BookSortUpFloorRequest : ApiRequestBase
	{
	}
}
