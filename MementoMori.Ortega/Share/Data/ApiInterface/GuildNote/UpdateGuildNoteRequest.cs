using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote;

[MessagePackObject(true)]
[OrtegaApi("guildNote/updateGuildNote", true, false)]
public class UpdateGuildNoteRequest : ApiRequestBase
{
    public long GuildNoteId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public GuildNoteCategoryType GuildNoteCategoryType { get; set; }
}
