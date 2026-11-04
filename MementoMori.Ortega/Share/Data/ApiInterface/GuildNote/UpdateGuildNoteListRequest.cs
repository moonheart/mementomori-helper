using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote;

[MessagePackObject(true)]
[OrtegaApi("guildNote/updateGuildNoteList", true, false)]
public class UpdateGuildNoteListRequest : ApiRequestBase
{
    public List<long> BeforeUpdateGuildNoteIdList { get; set; }

    public List<long> AfterUpdateGuildNoteIdList { get; set; }
}
