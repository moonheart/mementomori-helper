using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class GuildChatInfo
	{
		[Key(0)]
		public ChatInfo ChatInfo { get; set; }

		[Key(1)]
		public ChatReactionType MyChatReactionType { get; set; }

		[Key(2)]
		public Dictionary<ChatReactionType, int> ChatReactionCountMap { get; set; }

		[Key(3)]
		public bool CanReact { get; set; }

		[Key(4)]
		public bool IsAnnounced { get; set; }
	}
}
