using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LegendLeague
{
	[MessagePackObject(true)]
	[OrtegaApi("legendLeague/buyLegendLeagueIconReward", true, false)]
	public class BuyLegendLeagueIconRewardRequest : ApiRequestBase
	{
		public long IconRewardId { get; set; }
		public int BuyCount { get; set; }
	}

	[MessagePackObject(true)]
	public class BuyLegendLeagueIconRewardResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
