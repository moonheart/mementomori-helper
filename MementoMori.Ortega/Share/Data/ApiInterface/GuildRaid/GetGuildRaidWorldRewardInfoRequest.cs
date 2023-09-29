using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("guildRaid/getGuildRaidWorldRewardInfo", true, false)]
	public class GetGuildRaidWorldRewardInfoRequest : ApiRequestBase
	{
		public long GuildRaidBossId { get; set; }
	}
}
