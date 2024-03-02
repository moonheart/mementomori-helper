using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.DungeonBattle;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class SkipDungeonBattleResponse : ApiResponseBase, IUserSyncApiResponse
	{
        public List<UserItem> RewardItemList { get; set; }

        public List<long> RewardRelicIdList { get; set; }

		public DungeonBattleLayer CurrentDungeonBattleLayer { get; set; }

		public UserDungeonBattleShopDtoInfo UserDungeonBattleShopDtoInfo { get; set; }

		public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
