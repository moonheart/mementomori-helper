using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.PopularityVote
{
	[MessagePackObject(true)]
	public class ResultPresentationSetting
	{
		public PopularityPresentationVoteType PopularityPresentationVoteType { get; set; }

		public int RankingDisplayRange { get; set; }

		public int VoteCountDisplayRange { get; set; }

		public bool IsRandomDisplay { get; set; }
	}
}
