using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidDamageBarReward
	{
		public int DamageBarCount { get; set; }

		public int HolySteelCount { get; set; }
	}
}
