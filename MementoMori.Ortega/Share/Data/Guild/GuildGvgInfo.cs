using MementoMori.Ortega.Share.Data.Gvg;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Guild;

[MessagePackObject(true)]
public class GuildGvgInfo
{
    public List<CastleRewardInfo> CanGetCastleRewardInfoList { get; set; }
    public int CastleCountLarge { get; set; }
    public int CastleCountMedium { get; set; }
    public int CastleCountSmall { get; set; }
    public long CurrentRanking { get; set; }
    public List<CastleRewardInfo> GotCastleRewardInfoList { get; set; }
    public bool IsOpen { get; set; }
    public int MinCharacterNumLarge { get; set; }
    public int MinCharacterNumMedium { get; set; }
    public int MinCharacterNumSmall { get; set; }
    public int RemainingDeclarationCount { get; set; }
    public long RewardLimitTime { get; set; }
    public long TodayRanking { get; set; }
}