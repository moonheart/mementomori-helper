using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	public class StartGuildRaidResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public BattleResult BattleResult { get; set; }

		public BattleRewardResult BattleRewardResult { get; set; }

		public UserGuildRaidDtoInfo UserGuildRaidDtoInfo { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
