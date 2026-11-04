using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSort
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSort/getInfo", true, false)]
	public class BookSortGetInfoRequest : ApiRequestBase
	{
	}
}
