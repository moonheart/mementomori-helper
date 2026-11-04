using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/refreshPersonalMission", true, false)]
	public class UnionMissionRefreshPersonalMissionRequest : ApiRequestBase
	{
		public long MissionPanelId { get; set; }
	}
}
