using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LevelLink
{
	[MessagePackObject(true)]
	[OrtegaApi("levelLink/addExp", true, false)]
	public class AddExpRequest : ApiRequestBase
	{
	}

	[MessagePackObject(true)]
	public class AddExpResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
