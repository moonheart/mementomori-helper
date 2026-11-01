using MementoMori.Ortega.Share.Data.UnionMission;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission;

[MessagePackObject(true)]
public class UnionMissionRefreshSharedMissionResponse : ApiResponseBase
{
    public List<UnionMissionPanelInfo> SharedMissionPanelList { get; set; }

    public List<UnionMissionPanelInfo> PersonalMissionPanelList { get; set; }
}
