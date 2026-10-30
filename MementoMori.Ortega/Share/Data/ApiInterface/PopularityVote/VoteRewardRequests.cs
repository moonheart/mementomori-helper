using MessagePack;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Ortega.Share.Data.ApiInterface.PopularityVote
{
	[MessagePackObject(true)]
	[OrtegaApi("popularityVote/getInfo", true, false)]
	public class GetInfoRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class GetInfoResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<PopularityVoteRewardInfo> PopularityVoteRewardInfoList { get; set; }
		public long TotalVoteCount { get; set; }
		public UserSyncData UserSyncData { get; set; }
	}

	[MessagePackObject(true)]
	public class PopularityVoteRewardInfo
	{
		public long GoalVoteCount { get; set; }
		public bool IsReceived { get; set; }
	}

	[MessagePackObject(true)]
	[OrtegaApi("popularityVote/rewardVoteItem", true, false)]
	public class RewardVoteItemRequest : ApiRequestBase
	{
		public bool IsAllReward { get; set; }
		public long TargetRewardVoteCount { get; set; }
	}

	[MessagePackObject(true)]
	public class RewardVoteItemResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<PopularityVoteRewardInfo> PopularityVoteRewardInfoList { get; set; }
		public List<UserItem> RewardItemList { get; set; }
		public UserSyncData UserSyncData { get; set; }
	}
}
