using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class BuyProductWithWebCrystalResponse : ApiResponseBase, IUserSyncApiResponse
{
    public AcquisitionShopRewardInfo RewardInfo { get; set; }

    public ShopProductInfo ShopProductInfo { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
