using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/removeFriend", true, false)]
	public class RemoveFriendRequest : ApiRequestBase
	{
		public long TargetPlayerId { get; set; }
	}
}
