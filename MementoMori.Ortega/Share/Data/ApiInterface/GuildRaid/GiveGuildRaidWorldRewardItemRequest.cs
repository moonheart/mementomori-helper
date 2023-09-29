using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("guildRaid/giveGuildRaidWorldRewardItem", true, false)]
	public class GiveGuildRaidWorldRewardItemRequest : ApiRequestBase
	{
		public long GuildRaidBossId { get; set; }

		public long GoalDamage { get; set; }
	}
}
