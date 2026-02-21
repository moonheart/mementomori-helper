using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BookSortAssistance;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAssistanceAddAssistanceResponse : ApiResponseBase
	{
		public List<UserBookSortAssistanceQuest> UserBookSortAssistanceQuestList { get; set; }

		public List<long> AddedAssistanceConditionCharacterIdList { get; set; }
	}
}
