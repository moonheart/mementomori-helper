using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LuckyChance;

[MessagePackObject(true)]
public class LuckyChanceRegisteredPrizeItemInfo
{
    public UserItem UserItem { get; set; }

    public long CharacterId { get; set; }
}
