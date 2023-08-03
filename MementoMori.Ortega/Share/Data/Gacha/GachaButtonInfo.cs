using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaButtonInfo
{
    public UserItem ConsumeUserItem { get; set; }

    public int DisplayOrder { get; set; }

    public long GachaButtonId { get; set; }

    public long LotteryCount { get; set; }
}