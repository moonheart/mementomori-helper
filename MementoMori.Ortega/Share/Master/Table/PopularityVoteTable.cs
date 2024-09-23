using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class PopularityVoteTable : TableBase<PopularityVoteMB>
	{
		public PopularityVoteMB GetByInTime(DateTime jstDateTime)
		{
			return null;
		}

		public PopularityVoteMB GetByMissionInTime(DateTime localDateTime)
		{
			return null;
		}

		public PopularityVoteType GetPopularityVoteType(PopularityVoteMB popularityVoteMB, DateTime nowJstDateTime)
		{
			return PopularityVoteType.None;
		}

		public PopularityPresentationVoteType GetPopularityPresentationVoteType(PopularityVoteMB popularityVoteMB, DateTime nowJstDateTime)
		{
			return PopularityPresentationVoteType.None;
		}
	}
}
