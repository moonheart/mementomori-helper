using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	public class ReplyAllFriendResponse : ApiResponseBase
	{
		public int ProcessedNum { get; set; }
	}
}
