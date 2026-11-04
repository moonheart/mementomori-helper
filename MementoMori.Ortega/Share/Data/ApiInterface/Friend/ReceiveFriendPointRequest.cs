using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/receiveFriendPoint", true, false)]
	public class ReceiveFriendPointRequest : ApiRequestBase
	{
		public long SendPlayerId { get; set; }
	}
}
