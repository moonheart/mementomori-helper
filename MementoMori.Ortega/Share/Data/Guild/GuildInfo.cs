using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild
{
	[MessagePackObject(true)]
	public class GuildInfo
	{
		public long GuildExp { get; set; }

		public long GuildId { get; set; }

		public long GuildLevel { get; set; }

		public long GuildFame { get; set; }

		public long GuildMemberCount { get; set; }

		public GuildOverView GuildOverView{ get; set; }

		public PlayerInfo LeaderPlayerInfo{ get; set; }
	}
}
