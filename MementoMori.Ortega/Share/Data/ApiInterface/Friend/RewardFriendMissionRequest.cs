using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	[OrtegaApi("friend/rewardFriendMission", true, false)]
	public class RewardFriendMissionRequest : ApiRequestBase
	{
		public long FriendCampaignId { get; set; }

		public MissionAchievementType AchievementType { get; set; }

		public long FriendMissionId { get; set; }
	}
}
