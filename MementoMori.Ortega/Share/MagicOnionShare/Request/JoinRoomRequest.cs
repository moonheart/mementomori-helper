using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class JoinRoomRequest
	{
		[Key(0)]
		public int Password { get; set; }

		[Key(1)]
		public string RoomId { get; set; }
	}
}
