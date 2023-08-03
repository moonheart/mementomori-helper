using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[Obsolete("1.2.9で削除予定")]
	[MessagePackObject(true)]
	public class GuildRaidGuildDamageBarReward : GuildRaidDamageBarReward
	{
		public long GoldRewardCount { get; set; }
	}
}
