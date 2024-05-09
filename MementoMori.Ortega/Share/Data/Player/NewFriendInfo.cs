using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Player
{
	[MessagePackObject(true)]
	public class NewFriendInfo
	{
		public FriendInfoType FriendType { get; set; }

		public long PlayerId { get; set; }
	}
}
