using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	[OrtegaApi("guildTower/getGuildTowerRanking", true, false)]
	public class GetGuildTowerRankingRequest : ApiRequestBase
	{
	}
}
