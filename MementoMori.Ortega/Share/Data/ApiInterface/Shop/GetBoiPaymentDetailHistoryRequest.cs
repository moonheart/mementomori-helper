using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
[OrtegaApi("shop/getBoiPaymentDetailHistory", true, false)]
public class GetBoiPaymentDetailHistoryRequest : ApiRequestBase
{
    public string TransactionId { get; set; }
}
