using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.UnionMission;

[MessagePackObject(true)]
public class UnionMissionGuildRankingEntry : IPlayerIconInfo
{
    [Description("ギルドID。NPCは0")]
    public long GuildId { get; set; }

    [Description("ギルド名")]
    public string GuildName { get; set; }

    [Description("ギルドの累計獲得ポイント")]
    public long Point { get; set; }

    [Description("順位")]
    public long Rank { get; set; }

    [Description("当該ギルドの現在のメンバー数")]
    public int GuildMemberCount { get; set; }

    [Description("当該ギルドのメンバー数上限")]
    public long NumberOfPossibleJoinPeople { get; set; }

    [Description("プレイヤーアイコン枠情報(ギルドマスター)")]
    public LegendLeagueClassType LegendLeagueClass { get; set; }

    [Description("ギルドマスターのメインアイコンキャラクター")]
    public long MainCharacterIconId { get; set; }

    [Description("ギルドマスターのメインアイコンエフェクト")]
    public long MainCharacterIconEffectId { get; set; }

    long IPlayerIconInfo.GetIconId()
    {
        return MainCharacterIconId;
    }

    long IPlayerIconInfo.GetIconEffectId()
    {
        return MainCharacterIconEffectId;
    }

    LegendLeagueClassType IPlayerIconInfo.GetLegendLeagueClass()
    {
        return LegendLeagueClass;
    }
}
