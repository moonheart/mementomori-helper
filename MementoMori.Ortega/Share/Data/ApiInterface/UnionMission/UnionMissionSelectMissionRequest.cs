using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/selectMission", true, false)]
	public class UnionMissionSelectMissionRequest : ApiRequestBase
	{
		public long MissionId { get; set; }

		public long MissionPanelId { get; set; }
	}
}
