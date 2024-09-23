using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.PopularityVote
{
	[MessagePackObject(true)]
	public class PopularityVoteReward
	{
		public int VoteCount { get; set; }

		[Nest(true, 1)]
		public List<UserItem> VoteRewardItems { get; set; }
	}
}
