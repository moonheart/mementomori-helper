using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class ResetCastleMemoRequest
	{
		[Key(0)]
		public long CastleId { get; set; }
	}
}
