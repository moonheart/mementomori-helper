using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidDamageGaugeData
	{
		public long LeftDamage { get; set; }

		public int MaxGaugeCount { get; set; }
	}
}
