using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Chat;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnReceiveMessageResponse
	{
		[Key(0)]
		public ChatInfo ChatInfo { get; set; }

		[Key(1)]
		public long OtherPlayerId { get; set; }
	}
}
