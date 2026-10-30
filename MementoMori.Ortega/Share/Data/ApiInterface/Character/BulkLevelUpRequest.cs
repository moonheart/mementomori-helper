using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	[OrtegaApi("character/bulkLevelUp", true, false)]
	public class BulkLevelUpRequest : ApiRequestBase
	{
		public long DestinationLevel { get; set; }
		public string UserCharacterGuid { get; set; }
	}

	[MessagePackObject(true)]
	public class BulkLevelUpResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
