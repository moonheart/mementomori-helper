using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.WebStoreLoginCampaign;

[MessagePackObject(true)]
public class RewardLoginCampaignItemResponse : ApiResponseBase, IUserSyncApiResponse
{
    public List<RarityItem> RewardItemList { get; set; }

    public UserSyncData UserSyncData { get; set; }
}
