using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/replyAllFriend", true, false)]
	public class ReplyAllFriendRequest : ApiRequestBase
	{
		public bool IsApproval { get; set; }
	}
}
