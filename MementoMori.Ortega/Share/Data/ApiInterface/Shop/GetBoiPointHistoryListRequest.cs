using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop
{
	[MessagePackObject(true)]
	[OrtegaApi("shop/getBoiPointHistoryList", true, false)]
	public class GetBoiPointHistoryListRequest : ApiRequestBase
	{
	}
}
