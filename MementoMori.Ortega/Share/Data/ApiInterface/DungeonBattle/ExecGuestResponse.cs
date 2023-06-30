using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class ExecGuestResponse : ApiResponseBase
	{
		public UserDungeonBattleCharacterDtoInfo UserDungeonBattleCharacterDtoInfo { get; set; }

		public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo { get; set; }
	}
}
