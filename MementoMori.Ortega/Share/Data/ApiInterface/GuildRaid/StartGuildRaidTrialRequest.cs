using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("guildRaid/startGuildRaidTrial", true, false)]
	public class StartGuildRaidTrialRequest : ApiRequestBase
	{
		public long BelongGuildId { get; set; }
	}
}
