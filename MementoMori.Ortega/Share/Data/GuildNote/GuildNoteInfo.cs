using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildNote;

[MessagePackObject(true)]
public class GuildNoteInfo : IPlayerIconInfo
{
    public long GuildNoteId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public GuildNoteCategoryType GuildNoteCategoryType { get; set; }

    public long IconId { get; set; }

    public long IconEffectId { get; set; }

    public LegendLeagueClassType LegendLeagueClass { get; set; }

    public long PlayerId { get; set; }

    public string PlayerName { get; set; }

    public bool IsDeletedPlayer { get; set; }

    public ChatReactionType MyChatReactionType { get; set; }

    public Dictionary<ChatReactionType, int> ChatReactionCountMap { get; set; }

    public long UpdateLocalTimeStamp { get; set; }

    public int Order { get; set; }

    long IPlayerIconInfo.GetIconId()
    {
        return IconId;
    }

    long IPlayerIconInfo.GetIconEffectId()
    {
        return IconEffectId;
    }

    LegendLeagueClassType IPlayerIconInfo.GetLegendLeagueClass()
    {
        return LegendLeagueClass;
    }
}
