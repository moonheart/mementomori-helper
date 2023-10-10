using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild
{
	[MessagePackObject(true)]
	[OrtegaApi("guildInfo/getGuildMemberInfo", true, false)]
	public class GetGuildMemberInfoRequest : ApiRequestBase
	{
		public long GuildId { get; set; }
	}
}
