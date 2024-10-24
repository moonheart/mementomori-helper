using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsPvpWinContinueCountRankingData
{
    public int Rank { get; set; }

    public long PlayerId { get; set; }

    public string PlayerName { get; set; }

    public int WinContinueCount { get; set; }
}