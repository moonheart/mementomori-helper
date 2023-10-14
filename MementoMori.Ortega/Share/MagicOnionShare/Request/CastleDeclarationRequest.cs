using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class CastleDeclarationRequest
	{
		[Key(0)]
		public long CastleId { get; set; }
	}
}
