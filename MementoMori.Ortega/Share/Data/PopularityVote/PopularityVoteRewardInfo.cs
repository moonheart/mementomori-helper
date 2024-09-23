using MessagePack;

namespace MementoMori.Ortega.Share.Data.PopularityVote
{
	[MessagePackObject(true)]
	public class PopularityVoteRewardInfo
	{
		public long GoalVoteCount { get; set; }

		public bool IsReceived { get; set; }
	}
}
