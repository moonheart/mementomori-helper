using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class ChangeChatOptionInfo
	{
		[Key(1)]
		public bool CanReact { get; set; }

		[Key(0)]
		public ChatIdentityInfo ChatIdentityInfo { get; set; }

		[Key(2)]
		public bool IsAnnounced { get; set; }
	}
}
