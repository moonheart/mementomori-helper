using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildTower
{
	[MessagePackObject(true)]
	public class GuildTowerBadgeInfo
	{
		public long CurrentFloorId { get; set; }

		public int TodayTotalGuildWinCount { get; set; }

		public int TodayWinCount { get; set; }
	}
}
