using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class BulkBuyProductWithWebCrystalResponse : ApiResponseBase, IUserSyncApiResponse
{
    public List<BulkShopRewardInfo> BulkShopRewardInfos { get; set; }

    public List<ShopProductInfo> ShopProductInfos { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
