using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild
{
	[MessagePackObject(true)]
	public class GuildRank
	{
		public long BattlePower { get; set; }

		public GuildInfo GuildInfo{ get; set; }

		public long GuildStock { get; set; }

        public long GuildTowerMaxFloor { get; set; }

		public bool IsApplying { get; set; }

		public long Rank { get; set; }
	}
}
