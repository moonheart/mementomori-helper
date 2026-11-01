using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote;

[MessagePackObject(true)]
public class ShareGuildNoteResponse : ApiResponseBase, IUserSyncApiResponse
{
    public UserSyncData UserSyncData { get; set; }
}
