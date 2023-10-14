using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("localRaid/getLocalRaidInfo", true, false)]
	public class GetLocalRaidInfoRequest : ApiRequestBase
	{
	}
}
