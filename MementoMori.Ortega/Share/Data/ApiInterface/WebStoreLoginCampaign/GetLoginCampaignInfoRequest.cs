using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.WebStoreLoginCampaign
{
	[MessagePackObject(true)]
	[OrtegaApi("webStore/getLoginCampaignInfo", true, false)]
	public class GetLoginCampaignInfoRequest : ApiRequestBase
	{
	}
}
