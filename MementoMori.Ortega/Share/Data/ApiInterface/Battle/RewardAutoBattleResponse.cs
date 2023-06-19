using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class RewardAutoBattleResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public AutoBattleRewardResult AutoBattleRewardResult { get; set; }

		public UserSyncData UserSyncData { get; set; }

		public RewardAutoBattleResponse()
		{
		}
	}
}
