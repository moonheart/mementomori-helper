using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/acceptMission", true, false)]
	public class UnionMissionAcceptMissionRequest : ApiRequestBase
	{
		public long MissionPanelId { get; set; }
	}
}
