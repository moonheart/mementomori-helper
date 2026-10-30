using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class ReactChatInfo
	{
        [Key(0)]
        public ChatIdentityInfo ChatIdentityInfo { get; set; }

        [Key(1)]
		public ChatReactionType ChatReactionType { get; set; }

		[Key(2)]
		public bool IsCanceled { get; set; }

		[Key(3)]
		public long ReactPlayerId { get; set; }
	}
}
