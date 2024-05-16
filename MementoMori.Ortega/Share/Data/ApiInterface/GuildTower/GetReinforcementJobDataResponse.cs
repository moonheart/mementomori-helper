using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.GuildTower;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	public class GetReinforcementJobDataResponse : ApiResponseBase
	{
		public Dictionary<JobFlags, List<GuildTowerReinforcementJobRankingData>> RankingDataListMap { get; set; }

		public List<GuildTowerReinforcementJobData> ReinforcementJobDataList { get; set; }
	}
}
