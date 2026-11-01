using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildNote;

[MessagePackObject(true)]
public class ChatGuildNoteInfo
{
    public long GuildNoteId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }
}
