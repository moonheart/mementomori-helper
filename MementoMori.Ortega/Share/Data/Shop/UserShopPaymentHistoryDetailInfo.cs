using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop;

[MessagePackObject(true)]
public class UserShopPaymentHistoryDetailInfo
{
    public DateTime BuyDateTime { get; set; }

    public string TransactionId { get; set; }

    public PurchasePointGroupType PurchasePointGroupType { get; set; }

    public long Price { get; set; }

    public long UsePoint { get; set; }

    public long DiscountPrice { get; set; }

    public ShopPaymentStateType ShopPaymentStateType { get; set; }

    public ShopPayType ShopPayType { get; set; }

    public long SavePoint { get; set; }

    public long WorldId { get; set; }

    public List<BulkShopRewardInfo> BulkShopRewardInfos { get; set; }
}
