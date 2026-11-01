using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
[OrtegaApi("shop/buyProductWithWebCrystal", true, false)]
public class BuyProductWithWebCrystalRequest : ApiRequestBase
{
    public long MbId { get; set; }

    public string ProductId { get; set; }

    public ShopProductType ShopProductType { get; set; }
}
