using MementoMori.Ortega.Share.Data.Chat;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Common.Data
{
	public class ChatOptionData
	{
		public ChatOptionData(ChatIdentityInfo chatIdentityInfo)
		{
            this.ChatIdentityInfo = chatIdentityInfo;
		}

		public ChatOptionData(ChatInfo chatInfo)
		{
			ChatIdentityInfo chatIdentityInfo = chatInfo.GetChatIdentityInfo();
			this.ChatIdentityInfo = chatIdentityInfo;
			throw new NullReferenceException();
		}

		public ChatOptionData(GuildChatInfo guildChatInfo)
		{
            this.ChatIdentityInfo = guildChatInfo.ChatInfo.GetChatIdentityInfo();
            this.MyChatReactionType = guildChatInfo.MyChatReactionType;
            this.ChatReactionCountMap = guildChatInfo.ChatReactionCountMap;
            this.IsAnnounced = guildChatInfo.IsAnnounced;
		}

		public ChatIdentityInfo ChatIdentityInfo;

		public ChatReactionType MyChatReactionType;

		public Dictionary<ChatReactionType, int> ChatReactionCountMap;

		public bool IsAnnounced;
	}
}
