using MementoMori.Ortega.Share.Data.Shop;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
public class GetBoiPaymentDetailHistoryResponse : ApiResponseBase
{
    public UserShopPaymentHistoryDetailInfo UserShopPaymentHistoryDetailInfo { get; set; }
}
