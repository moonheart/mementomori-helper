using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class InviteRefuseRequest
	{
		[Key(0)]
		public long InvitedFriendPlayerId { get; set; }
	}
}
