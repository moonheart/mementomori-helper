using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Mission;
using MementoMori.Ortega.Share.Data.MyPage;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Mission
{
	[MessagePackObject(true)]
	public class RewardMissionResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public MissionGuideInfo MissionGuideInfo { get; set; }

		public AcquisitionMissionRewardInfo RewardInfo { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
