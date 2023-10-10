using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("guildRaid/openGuildRaid", true, false)]
	public class OpenGuildRaidRequest : ApiRequestBase
	{
		public long BelongGuildId { get; set; }

		public GuildRaidBossType GuildRaidBossType { get; set; }
	}
}
