using MessagePack;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Ortega.Share.Data.ApiInterface.RentalRaid
{
	[MessagePackObject(true)]
	[OrtegaApi("rentalRaid/getShareRewardInfo", true, false)]
	public class GetShareRewardInfoRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class GetShareRewardInfoResponse : ApiResponseBase
	{
		public int CurrentSharePoint { get; set; }
		public List<ShareRewardInfo> ShareRewardInfoList { get; set; }
	}

	[MessagePackObject(true)]
	public class ShareRewardInfo
	{
		public int RequiredSharePoint { get; set; }
		public List<UserItem> RewardItems { get; set; }
		public bool IsReceived { get; set; }
	}

	[MessagePackObject(true)]
	[OrtegaApi("rentalRaid/receiveShareReward", true, false)]
	public class ReceiveShareRewardRequest : ApiRequestBase
	{
		public int SharePoint { get; set; }
	}

	[MessagePackObject(true)]
	public class ReceiveShareRewardResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserItem> ReceiveItems { get; set; }
		public UserSyncData UserSyncData { get; set; }
	}
}
