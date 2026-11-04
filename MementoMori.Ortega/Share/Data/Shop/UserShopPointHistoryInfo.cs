using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop;

[MessagePackObject(true)]
public class UserShopPointHistoryInfo
{
    public DateTime BuyDateTime { get; set; }

    public ShopPointHistoryType ShopPointHistoryType { get; set; }

    public long ChangePoint { get; set; }

    public string ProductNameKey { get; set; }

    public PurchasePointGroupType PurchasePointGroupType { get; set; }

    public int BuyCount { get; set; }
}
