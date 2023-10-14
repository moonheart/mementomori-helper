using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class SendMessageRequest
	{
		[Key(0)]
		public ChatType ChatType { get; set; }

		[Key(1)]
		public string Message { get; set; }

		[Key(2)]
		public long ToPlayerId { get; set; }
	}
}
