using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class BossResponse : ApiErrorResponse, IUserSyncApiResponse
	{
		public BattleResult BattleResult { get; set; }

		public BattleRewardResult BattleRewardResult { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
