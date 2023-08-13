using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Mission
{
	[MessagePackObject(true)]
	[OrtegaApi("mission/rewardMissionActivity", true, false)]
	public class RewardMissionActivityRequest : ApiRequestBase
	{
		public MissionGroupType MissionGroupType { get; set; }

		public long RequiredCount { get; set; }
	}
}
