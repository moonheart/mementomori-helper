using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.ApiInterface.UnionMission
{
	[MessagePackObject(true)]
	[OrtegaApi("unionMission/receiveIndividualTotalPointReward", true, false)]
	public class UnionMissionReceiveIndividualTotalPointRewardRequest : ApiRequestBase
	{
		public long RewardMBId { get; set; }

		public UnionMissionTotalPointRewardType RewardType { get; set; }

		public bool IsRepeatPersonalReward { get; set; }
	}
}
