using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnNoticePrivateMessageResponse
	{
		[Key(0)]
		public long PlayerId { get; set; }
	}
}
