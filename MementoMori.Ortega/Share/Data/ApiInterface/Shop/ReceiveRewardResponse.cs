using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class ReceiveRewardResponse : ApiResponseBase, IUserSyncApiResponse
{
    public AcquisitionShopRewardInfo RewardInfo { get; set; }

    public ShopProductInfo ShopProductInfo { get; set; }

    public UserSyncData UserSyncData { get; set; }
}