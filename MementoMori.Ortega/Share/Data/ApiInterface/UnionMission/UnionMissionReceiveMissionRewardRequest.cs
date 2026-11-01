using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/receiveMissionReward", true, false)]
	public class UnionMissionReceiveMissionRewardRequest : ApiRequestBase
	{
		public long MissionPanelId { get; set; }
	}
}
