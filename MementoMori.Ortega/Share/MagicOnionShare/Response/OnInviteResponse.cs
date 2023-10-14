using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class OnInviteResponse
	{
		[Key(0)]
		public int ClearCount { get; set; }

		[Key(1)]
		public long PlayerId { get; set; }

		[Key(2)]
		public string PlayerName { get; set; }

		[Key(3)]
		public int PlayerRank { get; set; }

		[Key(4)]
		public long BattlePower { get; set; }

		[Key(5)]
		public string GuildName { get; set; }

		[Key(6)]
		public long QuestId { get; set; }

		[Key(7)]
		public string RoomId { get; set; }

		[Key(8)]
		public long CharacterIconId { get; set; }
	}
}
