using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class BossQuickResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public BattleRewardResult BattleRewardResult { get; set; }

		public UserSyncData UserSyncData { get; set; }

		public BossQuickResponse()
		{
		}
	}
}
