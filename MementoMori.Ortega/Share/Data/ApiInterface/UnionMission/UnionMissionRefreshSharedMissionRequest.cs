using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/refreshSharedMission", true, false)]
	public class UnionMissionRefreshSharedMissionRequest : ApiRequestBase
	{
		public long MissionPanelId { get; set; }
	}
}
