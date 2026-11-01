using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop
{
	[MessagePackObject(true)]
	[OrtegaApi("shop/getBoiPaymentHistoryList", true, false)]
	public class GetBoiPaymentHistoryListRequest : ApiRequestBase
	{
	}
}
