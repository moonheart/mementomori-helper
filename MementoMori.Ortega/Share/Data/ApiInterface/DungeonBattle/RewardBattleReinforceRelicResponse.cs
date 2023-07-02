using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class RewardBattleReinforceRelicResponse : ApiResponseBase
	{
		public long AddedRelicId { get; set; }

		public long ReinforcedAfterRelicId { get; set; }

		public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo{ get; set; }
	}
}
