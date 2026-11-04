using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Shop
{
	[MessagePackObject(true)]
	[OrtegaApi("shop/getWebStoreGuidancePanel", true, false)]
	public class GetWebStoreGuidancePanelRequest : ApiRequestBase
	{
	}
}
