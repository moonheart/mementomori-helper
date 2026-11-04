using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat;

[MessagePackObject(false)]
public class ChatRecruitGuildMemberInfo
{
    [Key(0)]
    public long GuildId { get; set; }

    [Key(1)]
    public string GuildName { get; set; }

    [Key(2)]
    public long GuildLv { get; set; }

    [Key(3)]
    public long IconId { get; set; }

    [Key(4)]
    public long IconEffectId { get; set; }

    [Key(5)]
    public LegendLeagueClassType LegendLeagueClass { get; set; }

    [Key(6)]
    public string RecruitMessage { get; set; }
}
