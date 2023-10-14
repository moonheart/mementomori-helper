using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class JoinRandomRoomRequest
	{
		[Key(0)]
		public long QuestId { get; set; }
	}
}
