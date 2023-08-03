using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild
{
	[OrtegaApi("guildInfo/getGuildBaseInfo", true, false)]
	[MessagePackObject(true)]
	public class GetGuildBaseInfoRequest : ApiRequestBase
	{
		public long BelongGuildId { get; set; }
	}
}
