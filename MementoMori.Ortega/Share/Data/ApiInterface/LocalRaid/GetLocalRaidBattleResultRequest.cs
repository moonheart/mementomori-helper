using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("localRaid/getLocalRaidBattleResult", true, false)]
	public class GetLocalRaidBattleResultRequest : ApiRequestBase
	{
	}
}
