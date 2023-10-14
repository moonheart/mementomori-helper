using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class RefuseRequest
	{
		[Key(0)]
		public long TargetPlayerId { get; set; }
	}
}
