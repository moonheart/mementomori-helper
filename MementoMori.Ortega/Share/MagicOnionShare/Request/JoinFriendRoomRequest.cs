using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class JoinFriendRoomRequest
	{
		[Key(0)]
		public string RoomId { get; set; }
	}
}
