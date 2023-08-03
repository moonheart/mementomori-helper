using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class GuildRaidDtoInfo
	{
		public GuildRaidBossType BossType { get; set; }

		public long CloseLimitTime { get; set; }

		public long LastReleaseTime { get; set; }

		public int TotalChallengeCount { get; set; }

		public long TotalDamage { get; set; }
	}
}
