using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("guildRaid/getGuildRaidInfo", true, false)]
	public class GetGuildRaidInfoRequest : ApiRequestBase
	{
		public long BelongGuildId { get; set; }
	}
}
