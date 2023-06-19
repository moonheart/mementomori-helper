using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.TowerBattle;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle
{
	[MessagePackObject(true)]
	public class StartResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public BattleResult BattleResult { get; set; }

		public BattleRewardResult BattleRewardResult { get; set; }

		public List<TowerBattleRewardsItem> TowerBattleRewardsItemList { get; set; }

		public UserSyncData UserSyncData { get; set; }

		public StartResponse()
		{
		}
	}
}
