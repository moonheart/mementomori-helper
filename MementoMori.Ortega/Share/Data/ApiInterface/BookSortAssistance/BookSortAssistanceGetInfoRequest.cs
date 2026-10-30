using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSortAssistance/getInfo", true, false)]
	public class BookSortAssistanceGetInfoRequest : ApiRequestBase
	{
	}
}
