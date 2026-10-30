using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LocalRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("localRaid/getLocalRaidFriendInfo", true, false)]
	public class GetLocalRaidFriendInfoRequest : ApiRequestBase
	{
	}
}
