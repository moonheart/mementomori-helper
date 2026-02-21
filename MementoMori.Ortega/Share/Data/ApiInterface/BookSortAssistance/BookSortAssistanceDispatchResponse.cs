using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BookSortAssistance;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAssistanceDispatchResponse : ApiResponseBase
	{
		public List<UserBookSortAssistanceQuest> UserBookSortAssistanceQuestList { get; set; }

		public UserBookSortAssistanceLv UserBookSortAssistanceLv { get; set; }
	}
}
