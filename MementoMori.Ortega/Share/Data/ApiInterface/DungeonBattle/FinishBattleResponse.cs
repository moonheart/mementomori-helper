using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class FinishBattleResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserDungeonBattleCharacterDtoInfo> UserDungeonBattleCharacterDtoInfos
		{
			get;
			set;
		}

		public UserSyncData UserSyncData
		{
			get;
			set;
		}

		public FinishBattleResponse()
		{
		}
	}
}
