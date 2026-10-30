using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.BookSortAssistance;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAssistanceRewardResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserBookSortAssistanceQuest> UserBookSortAssistanceQuestList { get; set; }

		public List<UserItem> RewardItems { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
