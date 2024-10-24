using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsHighlightPvpBattleData
{
    public WeeklyTopicsHighlightBattleLabelType LabelType { get; set; }

    public long EndTurnCount { get; set; }

    public PvpRankingPlayerInfo AttackerInfo { get; set; }

    public PvpRankingPlayerInfo DefenderInfo { get; set; }

    public long AttackerPlayerId { get; set; }

    public long DefenderPlayerId { get; set; }

    public string BattleToken { get; set; }
}