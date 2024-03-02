using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Guild;
using MementoMori.Ortega.Share.Data.GuildTower;
using MementoMori.Ortega.Share.Data.Player;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class GuildSyncData
	{
		public List<PlayerInfo> ApplyPlayerInfoList { get; set; }

        public long CreateGuildLocalTime { get; set; }

		public GlobalGvgGroupType GlobalGvgGroupType { get; set; }

		public string GuildAnnouncement { get; set; }

		public long GuildAnnouncementUpdateTime { get; set; }

		public long GuildBattlePower { get; set; }

		public GuildInfo GuildInfo { get; set; }

		public List<PlayerInfo> GuildPlayerInfoList { get; set; }
        
        public GuildTowerBadgeInfo GuildTowerBadgeInfo { get; set; }

		public long JoinGuildTime { get; set; }

		public int MatchingNumber { get; set; }

		public PlayerGuildPositionType PlayerGuildPositionType { get; set; }

		public GuildSyncData()
		{
			List<PlayerInfo> list = new List<PlayerInfo>();
			this.ApplyPlayerInfoList = list;
			List<PlayerInfo> list2 = new List<PlayerInfo>();
			this.GuildPlayerInfoList = list2;
		}
	}
}
