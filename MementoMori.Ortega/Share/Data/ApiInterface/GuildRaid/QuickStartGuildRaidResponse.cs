using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildRaid
{
	[MessagePackObject(true)]
	public class QuickStartGuildRaidResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public BattleRewardResult BattleRewardResult { get; set; }

		public BattleSimulationResult BattleSimulationResult { get; set; }

		public UserGuildRaidDtoInfo UserGuildRaidDtoInfo { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
