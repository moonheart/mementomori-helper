using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/cancelAllApplyFriend", true, false)]
	public class CancelAllApplyFriendRequest : ApiRequestBase
	{
	}
}
