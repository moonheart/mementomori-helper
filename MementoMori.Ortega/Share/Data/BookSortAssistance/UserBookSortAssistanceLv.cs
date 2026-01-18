using MessagePack;

namespace MementoMori.Ortega.Share.Data.BookSortAssistance
{
	[MessagePackObject(true)]
	public class UserBookSortAssistanceLv
	{
		public long AssistanceLv { get; set; }

		public long DispatchCount { get; set; }
	}
}
