using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop;

[MessagePackObject(true)]
public class UserShopPaymentHistoryInfo
{
    public PurchasePointGroupType PurchasePointGroupType { get; set; }

    public DateTime BuyDateTime { get; set; }

    public long DiscountPrice { get; set; }

    public string ProductNameKey { get; set; }

    public string TransactionId { get; set; }

    public ShopPaymentStateType ShopPaymentStateType { get; set; }

    public ShopPayType ShopPayType { get; set; }

    public int BuyCount { get; set; }

    public long PlayerId { get; set; }

    public long WorldId { get; set; }
}
