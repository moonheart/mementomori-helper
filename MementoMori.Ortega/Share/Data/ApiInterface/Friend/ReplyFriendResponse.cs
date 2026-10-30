using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	public class ReplyFriendResponse : ApiResponseBase
	{
		public long ErrorMessageId { get; set; }

		public bool IsSuccess { get; set; }
	}
}
