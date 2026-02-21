using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSortAssistance/addAssistance", true, false)]
	public class BookSortAssistanceAddAssistanceRequest : ApiRequestBase
	{
	}
}
