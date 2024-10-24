using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsPvpData
{
    public List<WeeklyTopicsPvpWinContinueCountRankingData> WinContinueCountRankingList { get; set; }

    public List<WeeklyTopicsCharacterUsageRankingData> RankerCharacterUsageRankingList { get; set; }

    public List<WeeklyTopicsCharacterUsageRankingData> TotalCharacterUsageRankingList { get; set; }

    public List<WeeklyTopicsHighlightPvpBattleData> HighlightPvpBattleDataList { get; set; }
}