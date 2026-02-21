using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BookSortAssistance;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSortAssistance/dispatch", true, false)]
	public class BookSortAssistanceDispatchRequest : ApiRequestBase
	{
		public List<BookSortAssistanceQuestStart> BookSortAssistanceStartList { get; set; }
	}
}
