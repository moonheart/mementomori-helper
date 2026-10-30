using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Player;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Ranking
{
	[MessagePackObject(true)]
	public class GetTowerRankingResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public Dictionary<TowerType, long> NowRankingMap { get; set; }

		public Dictionary<TowerType, long> TodayRankingMap { get; set; }

		public Dictionary<TowerType, List<PlayerInfo>> TopPlayerInfoListMap { get; set; }

		public Dictionary<TowerType, Dictionary<long, long>> TopPlayerMaxClearQuestIdMap { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
