using MessagePack;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Ortega.Share.Data.ApiInterface.WebStoreLoginCampaign
{
	[MessagePackObject(true)]
	[OrtegaApi("webStore/getLoginCampaignInfo", true, false)]
	public class GetLoginCampaignInfoRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class GetLoginCampaignInfoResponse : ApiResponseBase
	{
		public List<UserItem> ItemList { get; set; }
		public bool IsAchieved { get; set; }
	}

	[MessagePackObject(true)]
	[OrtegaApi("webStore/rewardLoginCampaignItem", true, false)]
	public class RewardLoginCampaignItemRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class RewardLoginCampaignItemResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserItem> RewardItemList { get; set; }
		public UserSyncData UserSyncData { get; set; }
	}
}
