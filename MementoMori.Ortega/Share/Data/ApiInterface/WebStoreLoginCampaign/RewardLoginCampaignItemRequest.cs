using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.WebStoreLoginCampaign
{
	[MessagePackObject(true)]
	[OrtegaApi("webStore/rewardLoginCampaignItem", true, false)]
	public class RewardLoginCampaignItemRequest : ApiRequestBase
	{
	}
}
