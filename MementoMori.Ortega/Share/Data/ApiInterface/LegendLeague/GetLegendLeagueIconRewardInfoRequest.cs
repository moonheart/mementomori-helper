using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LegendLeague
{
	[MessagePackObject(true)]
	[OrtegaApi("legendLeague/getLegendLeagueIconRewardInfo", true, false)]
	public class GetLegendLeagueIconRewardInfoRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class GetLegendLeagueIconRewardInfoResponse : ApiResponseBase
	{
		public bool IsCurrentTimeInPrevRankRewardPeriod { get; set; }
		public List<Data.LegendLeague.LegendLeagueIconReward> LegendLeagueIconRewardList { get; set; }
		public long SelectedIconRewardId { get; set; }
	}
}
