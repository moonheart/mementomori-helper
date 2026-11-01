using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/getRanking", true, false)]
	public class UnionMissionGetRankingRequest : ApiRequestBase
	{
	}
}
