using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/getInfo", true, false)]
	public class UnionMissionGetInfoRequest : ApiRequestBase
	{
	}
}
