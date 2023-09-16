using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TradeShop
{
	[MessagePackObject(true)]
	[OrtegaApi("tradeShop/getList", true, false)]
	public class GetListRequest : ApiRequestBase
	{
	}
}
