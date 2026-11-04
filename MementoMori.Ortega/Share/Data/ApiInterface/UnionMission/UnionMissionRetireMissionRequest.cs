using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/retireMission", true, false)]
	public class UnionMissionRetireMissionRequest : ApiRequestBase
	{
		public long MissionPanelId { get; set; }
	}
}
