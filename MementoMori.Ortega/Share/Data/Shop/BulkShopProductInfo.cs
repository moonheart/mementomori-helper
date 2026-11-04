using MementoMori.Ortega.Share.Enums;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.Shop;

[MessagePackObject(true)]
[Description("一括ショップ商品情報")]
public class BulkShopProductInfo
{
    public long MbId { get; set; }

    public string ProductId { get; set; }

    public ShopProductType ShopProductType { get; set; }

    public int Count { get; set; }
}
