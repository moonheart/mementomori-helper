using MementoMori.Ortega.Share.Data.GuildNote;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote;

[MessagePackObject(true)]
public class CreateGuildNoteResponse : ApiResponseBase, IUserSyncApiResponse
{
    public bool IsNgWordReplaced { get; set; }

    public List<GuildNoteInfo> GuildNoteInfoList { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
