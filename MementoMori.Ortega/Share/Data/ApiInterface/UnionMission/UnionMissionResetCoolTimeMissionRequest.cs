using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/resetCoolTimeMission", true, false)]
	public class UnionMissionResetCoolTimeMissionRequest : ApiRequestBase
	{
		public long MissionPanelId { get; set; }
	}
}
