using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Mission;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Mission
{
	[MessagePackObject(true)]
	public class RewardMissionActivityResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public AcquisitionMissionRewardInfo RewardInfo { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
