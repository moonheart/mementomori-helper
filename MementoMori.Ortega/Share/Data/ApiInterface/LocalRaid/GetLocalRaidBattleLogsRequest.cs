using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("localRaid/getLocalRaidBattleLogs", true, false)]
	public class GetLocalRaidBattleLogsRequest : ApiRequestBase
	{
	}
}
