using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(false)]
	public class ChatIdentityInfo
	{
		[Key(0)]
		public long SendLocalTimestamp { get; set; }

		[Key(1)]
		public long SendPlayerId { get; set; }

		public bool Equals(ChatIdentityInfo other)
		{
            if (other == null) return false;
            
            return this.SendLocalTimestamp == other.SendLocalTimestamp && this.SendPlayerId == other.SendPlayerId;
		}
	}
}
