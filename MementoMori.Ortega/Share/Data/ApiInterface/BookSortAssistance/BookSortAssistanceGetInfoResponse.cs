using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BookSortAssistance;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAssistanceGetInfoResponse : ApiResponseBase
	{
		public List<UserBookSortAssistanceQuest> UserBookSortAssistanceQuestList { get; set; }

		public long ChangeDayLocalTimeStamp { get; set; }

		public UserBookSortAssistanceLv UserBookSortAssistanceLv { get; set; }

		public long EndLocalTimeStamp { get; set; }

		public List<long> AddedAssistanceConditionCharacterIdList { get; set; }

		public long ShopProductEndLocalTimeStamp { get; set; }
	}
}
