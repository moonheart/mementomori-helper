using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Friend
{
	[MessagePackObject(true)]
	public class FriendStatusInfo
	{
		public long PlayerId { get; set; }

		public FriendStatusType FriendStatusType { get; set; }

		public bool IsAllowedFriendBattle { get; set; }

		public long LatestQuestId { get; set; }
	}
}
