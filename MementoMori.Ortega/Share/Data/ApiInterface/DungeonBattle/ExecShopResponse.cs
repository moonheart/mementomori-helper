using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class ExecShopResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo { get; set; }

		public UserDungeonBattleShopDtoInfo UserDungeonBattleShopDtoInfo { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
