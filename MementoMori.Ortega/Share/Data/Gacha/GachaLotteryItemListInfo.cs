using MementoMori.Ortega.Share.Extensions;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaLotteryItemListInfo : ILotteryWeight
{
    public long GachaLotteryItemListId { get; set; }

    public int LotteryWeight { get; set; }
}