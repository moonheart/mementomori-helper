using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/sendFriendPoint", true, false)]
	public class SendFriendPointRequest : ApiRequestBase
	{
		public long TargetPlayerId { get; set; }
	}
}
