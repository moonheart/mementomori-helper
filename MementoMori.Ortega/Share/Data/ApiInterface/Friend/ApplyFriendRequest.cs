using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/applyFriend", true, false)]
	public class ApplyFriendRequest : ApiRequestBase
	{
		public bool IsApply { get; set; }

		public long TargetPlayerId { get; set; }
	}
}
