using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop;

[MessagePackObject(true)]
[OrtegaApi("shop/getProductList", true, false)]
public class GetProductListRequest : ApiRequestBase
{
}