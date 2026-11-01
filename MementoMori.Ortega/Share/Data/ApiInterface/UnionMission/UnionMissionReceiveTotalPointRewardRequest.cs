using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/receiveTotalPointReward", true, false)]
	public class UnionMissionReceiveTotalPointRewardRequest : ApiRequestBase
	{
	}
}
