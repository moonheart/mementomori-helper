using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	[OrtegaApi("guildTower/getGuildTowerInfo", true, false)]
	public class GetGuildTowerInfoRequest : ApiRequestBase
	{
	}
}
