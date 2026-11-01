using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildNote;

[MessagePackObject(true)]
[OrtegaApi("guildNote/createGuildNote", true, false)]
public class CreateGuildNoteRequest : ApiRequestBase
{
    public string Title { get; set; }

    public string Content { get; set; }

    public GuildNoteCategoryType GuildNoteCategoryType { get; set; }
}
