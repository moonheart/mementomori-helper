using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsCharacterUsageRankingData
{
    public int Rank { get; set; }

    public long CharacterId { get; set; }

    public double UsageRate { get; set; }
}