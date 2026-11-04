using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LevelLink
{
	[MessagePackObject(true)]
	[OrtegaApi("levelLink/levelUp", true, false)]
	public class LevelUpRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class LevelUpResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
