using MementoMori.Ortega.Share.Data.GuildNote;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote;

[MessagePackObject(true)]
public class UpdateGuildNoteListResponse : ApiResponseBase, IUserSyncApiResponse
{
    public List<GuildNoteInfo> GuildNoteInfoList { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
