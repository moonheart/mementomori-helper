using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.WebStoreLoginCampaign;

[MessagePackObject(true)]
public class GetLoginCampaignInfoResponse : ApiResponseBase
{
    public List<RarityItem> ItemList { get; set; }

    public bool IsAchieved { get; set; }
}
