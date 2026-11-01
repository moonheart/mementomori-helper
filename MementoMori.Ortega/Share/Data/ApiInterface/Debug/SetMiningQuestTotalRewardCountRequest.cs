using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Debug
{
	[MessagePackObject(true)]
	[OrtegaApi("debug/setMiningQuestTotalRewardCount", true, false)]
	public class SetMiningQuestTotalRewardCountRequest : ApiRequestBase
	{
		public long TodayTotalRewardCount { get; set; }
	}
}
