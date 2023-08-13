using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserFriendDtoInfo
	{
		public long FriendPointSendDate { get; set; }

		public FriendStatusType FriendStatusType { get; set; }

		public bool IsChecked { get; set; }

		public bool IsReceived { get; set; }

		public long OtherPlayerId { get; set; }

		public long RegistrationDate { get; set; }
	}
}
