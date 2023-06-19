using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	public class QuickResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public AutoBattleRewardResult AutoBattleRewardResult { get; set; }

		public long QuickLastExecuteTime
		{
			get;
			set;
		}

		public long QuickTodayUseCurrencyCount
		{
			get;
			set;
		}

		public long QuickTodayUsePrivilegeCount
		{
			get;
			set;
		}

		public UserSyncData UserSyncData{ get; set; }

		public QuickResponse()
		{
		}
	}
}
