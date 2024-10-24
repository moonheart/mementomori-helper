using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsHighlightBossBattleData
{
    public WeeklyTopicsHighlightBattleLabelType LabelType { get; set; }

    public long QuestId { get; set; }

    public BossBattlePlayerInfo AttackerInfo { get; set; }

    public long AttackerPlayerId { get; set; }

    public List<BossDisplayInfo> BossDisplayInfoList { get; set; }

    public string BattleToken { get; set; }
}