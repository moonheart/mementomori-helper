using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	[OrtegaApi("character/levelUp", true, false)]
	public class LevelUpRequest : ApiRequestBase
	{
		public string UserCharacterGuid { get; set; }
	}

	[MessagePackObject(true)]
	public class LevelUpResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
