using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidUserRankingInfo
	{
		public LegendLeagueClassType LegendLeagueClass { get; set; }

		public long MainCharacterIconId { get; set; }

		public long PlayerId { get; set; }

		public string PlayerName{ get; set; }

		public long TotalDamage { get; set; }

		public float TotalDamagePercent { get; set; }
	}
}
