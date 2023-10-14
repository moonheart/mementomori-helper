using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class InviteRequest
	{
		[Key(0)]
		public long FriendPlayerId { get; set; }
	}
}
