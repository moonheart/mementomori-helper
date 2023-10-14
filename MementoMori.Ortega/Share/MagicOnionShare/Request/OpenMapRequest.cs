using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class OpenMapRequest
	{
		[Key(0)]
		public int MatchingNumber { get; set; }
	}
}
