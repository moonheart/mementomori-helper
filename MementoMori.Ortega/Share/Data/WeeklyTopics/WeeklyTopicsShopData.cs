using MementoMori.Ortega.Share.Data.TradeShop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.WeeklyTopics;

[MessagePackObject(true)]
public class WeeklyTopicsShopData
{
    public List<TradeShopItem> TradeShopItemList { get; set; }

    public long ExpirationTimeStamp { get; set; }
}