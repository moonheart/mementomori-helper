using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	public class BattleResponse : ApiResponseBase, IGuildSyncApiResponse, IUserSyncApiResponse
	{
		public BattleResult BattleResult { get; set; }

		public BattleRewardResult BattleRewardResult { get; set; }

		public GuildSyncData GuildSyncData { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
