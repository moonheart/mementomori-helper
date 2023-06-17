using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
    [Description("ゲリラパックランク種別")]
    public enum ShopGuerrillaPackRankType
    {
        [Description("120")] Rank1 = 1,
        [Description("610")] Rank2,
        [Description("980")] Rank3,
        [Description("1480")] Rank4,
        [Description("3060")] Rank5,
        [Description("6100")] Rank6,
        [Description("12000")] Rank7
    }
}