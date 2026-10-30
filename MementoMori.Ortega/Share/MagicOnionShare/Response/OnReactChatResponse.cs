using MementoMori.Ortega.Share.Data.Chat;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnReactChatResponse
	{
        [Key(0)]
        public List<ReactChatInfo> ReactChatInfoList { get; set; }
    }
}
