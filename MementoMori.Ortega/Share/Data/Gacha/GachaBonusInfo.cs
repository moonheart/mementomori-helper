using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaBonusInfo
{
    public int GachaBonusCount { get; set; }

    public UserItem GachaBonusItem { get; set; }

    public int LotteryProbability { get; set; }
}