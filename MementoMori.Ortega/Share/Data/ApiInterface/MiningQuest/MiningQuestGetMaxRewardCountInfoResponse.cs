using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestGetMaxRewardCountInfoResponse : ApiResponseBase
	{
		public long ChangeDayLocalTimeStamp { get; set; }

		public long TotalRewardCount { get; set; }

		public long MaxRewardCount { get; set; }

		public long CarryOverMaxRewardCount { get; set; }

		public int RewardCountLevel { get; set; }
	}
}
