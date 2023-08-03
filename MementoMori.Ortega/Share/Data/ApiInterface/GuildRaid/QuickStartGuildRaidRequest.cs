using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[OrtegaApi("guildRaid/quickStartGuildRaid", true, false)]
	[MessagePackObject(true)]
	public class QuickStartGuildRaidRequest : ApiRequestBase
	{
		public long BelongGuildId { get; set; }

		public GuildRaidBossType GuildRaidBossType { get; set; }
	}
}
