using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LuckyChance;

[MessagePackObject(true)]
public class LuckyChanceLotteryItemInfo : IDeepCopy<LuckyChanceLotteryItemInfo>
{
    public UserItem LotteryItem { get; set; }

    public int LotteryLimitCount { get; set; }

    public int RemainingCount { get; set; }

    public LuckyChanceLotteryItemInfo DeepCopy()
    {
        return new LuckyChanceLotteryItemInfo
        {
            LotteryItem = LotteryItem.DeepCopy(),
            LotteryLimitCount = LotteryLimitCount,
            RemainingCount = RemainingCount
        };
    }
}
