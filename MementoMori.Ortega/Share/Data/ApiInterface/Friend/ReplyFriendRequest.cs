using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/replyFriend", true, false)]
	public class ReplyFriendRequest : ApiRequestBase
	{
		public bool IsApproval { get; set; }

		public long TargetPlayerId { get; set; }
	}
}
