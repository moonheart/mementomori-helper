﻿using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class ChatInfo
	{
		[Key(0)]
		public long CharacterId { get; set; }

		[Key(1)]
		public ChatType ChatType { get; set; }

		[Key(8)]
		public LegendLeagueClassType LegendLeagueClass { get; set; }

		[Key(2)]
		public string Message { get; set; }

		[Key(3)]
		public long PlayerId { get; set; }

		[Key(4)]
		public string PlayerName { get; set; }

		[Key(10)]
		public string[] SystemChatMessageArgs { get; set; }

		[Key(9)]
		public SystemChatMessageIdType SystemChatMessageIdType { get; set; }

		[Key(5)]
		public string SystemChatMessageKey { get; set; }

		[Key(6)]
		public SystemChatType SystemChatType { get; set; }

        [Key(12)]
        public long BalloonItemId { get; set; }

		[Key(7)]
		public long LocalTimeStamp { get; set; }
	}
}
