using MessagePack;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Ortega.Share.Data.ApiInterface.EquipmentSynchro
{
	[MessagePackObject(true)]
	[OrtegaApi("equipmentSynchro/rewardMission", true, false)]
	public class EquipmentSynchroRewardMissionRequest : ApiRequestBase
	{
		public bool IsBulk { get; set; }
	}

	[MessagePackObject(true)]
	public class EquipmentSynchroRewardMissionResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserItem RewardItem { get; set; }
		public UserSyncData UserSyncData { get; set; }
	}
}
