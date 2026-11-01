using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.UnionMission;

[MessagePackObject(true)]
public class UnionMissionPersonalRankingEntry : IPlayerIconInfo
{
    [Description("累計獲得ポイント")]
    public long Point { get; set; }

    [Description("受注数")]
    public int AcceptedCount { get; set; }

    [Description("達成数")]
    public int AchievedCount { get; set; }

    [Description("プレイヤーID")]
    public long PlayerId { get; set; }

    [Description("プレイヤー表示名")]
    public string PlayerName { get; set; }

    [Description("順位")]
    public long Rank { get; set; }

    [Description("プレイヤーアイコン枠情報")]
    public LegendLeagueClassType LegendLeagueClass { get; set; }

    [Description("メインアイコンキャラクター")]
    public long MainCharacterIconId { get; set; }

    [Description("メインアイコンエフェクト")]
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
