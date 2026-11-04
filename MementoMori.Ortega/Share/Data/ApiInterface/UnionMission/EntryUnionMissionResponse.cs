using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission;

[MessagePackObject(true)]
public class EntryUnionMissionResponse : ApiResponseBase, IGuildSyncApiResponse
{
    public GuildSyncData GuildSyncData { get; set; }
}
