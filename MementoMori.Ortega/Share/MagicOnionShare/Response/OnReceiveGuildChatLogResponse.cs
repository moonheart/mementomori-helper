using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Chat;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnReceiveGuildChatLogResponse
	{
		[Key(0)]
		public List<ChatInfo> ChatInfoList { get; set; }
	}
}
