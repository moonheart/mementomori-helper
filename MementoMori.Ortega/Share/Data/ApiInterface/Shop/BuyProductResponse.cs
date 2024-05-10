using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class BuyProductResponse : ApiResponseBase, IUserSyncApiResponse
{
    public long GivenPlayerId { get; set; }

    public string GivenPlayerName { get; set; }

    public AcquisitionShopRewardInfo RewardInfo { get; set; }

    public ShopProductInfo ShopProductInfo { get; set; }
    
    public UserStripePointHistoryInfo UserStripePointHistoryInfo { get; set; }

    public UserSyncData UserSyncData { get; set; }
}