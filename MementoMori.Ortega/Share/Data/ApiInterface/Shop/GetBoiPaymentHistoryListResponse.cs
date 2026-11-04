using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class GetBoiPaymentHistoryListResponse : ApiResponseBase
{
    public List<UserShopPaymentHistoryInfo> UserShopPaymentHistoryInfos { get; set; }
}
