using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
[OrtegaApi("shop/bulkBuyProductWithWebCrystal", true, false)]
public class BulkBuyProductWithWebCrystalRequest : ApiRequestBase
{
    public List<BulkShopProductInfo> BulkShopProductInfos { get; set; }
}
