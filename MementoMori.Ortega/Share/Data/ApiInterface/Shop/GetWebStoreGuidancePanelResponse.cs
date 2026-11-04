using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class GetWebStoreGuidancePanelResponse : ApiResponseBase
{
    public ShopProductWebStoreGuidancePanel ShopProductWebStoreGuidancePanel { get; set; }
}
