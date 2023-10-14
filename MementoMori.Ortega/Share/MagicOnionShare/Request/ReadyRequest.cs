using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class ReadyRequest
	{
		[Key(0)]
		public bool IsReady { get; set; }
	}
}
