using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.BookSortAssistance
{
	[MessagePackObject(true)]
	[OrtegaApi("bookSortAssistance/reward", true, false)]
	public class BookSortAssistanceRewardRequest : ApiRequestBase
	{
		public List<string> UserBookSortAssistanceQuestGuidList { get; set; }
	}
}
