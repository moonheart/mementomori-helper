using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Guild
{
	[MessagePackObject(true)]
	[OrtegaApi("guildInfo/getGuildId", true, false)]
	public class GetGuildIdRequest : ApiRequestBase
	{
	}
}
