using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LevelLink
{
	[MessagePackObject(true)]
	[OrtegaApi("levelLink/resetCoolTime", true, false)]
	public class ResetCoolTimeRequest : ApiRequestBase
	{
		public int LevelLinkCellNo { get; set; }
	}

	[MessagePackObject(true)]
	public class ResetCoolTimeResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
