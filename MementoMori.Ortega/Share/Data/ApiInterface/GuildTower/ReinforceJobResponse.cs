using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.GuildTower;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	public class ReinforceJobResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserItem> NotConsumedItemList { get; set; }

		public List<GuildTowerReinforcementJobRankingData> RankingDataList { get; set; }

		public GuildTowerReinforcementJobData ReinforcementJobData { get; set; }

        public long LastReinforceLocalTimeStamp { get; set; }

        public UserSyncData UserSyncData { get; set; }
	}
}
