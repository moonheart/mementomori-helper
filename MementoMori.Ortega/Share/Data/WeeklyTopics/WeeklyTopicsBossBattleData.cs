using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsBossBattleData
{
    public Dictionary<long, int> ReachChapterCount { get; set; }

    public long MinFrontLineChapterId { get; set; }

    public List<WeeklyTopicsCharacterUsageRankingData> FrontLineCharacterUsageRankingList { get; set; }

    public List<WeeklyTopicsCharacterUsageRankingData> TotalCharacterUsageRankingList { get; set; }

    public List<WeeklyTopicsHighlightBossBattleData> HighlightBossBattleDataList { get; set; }
}