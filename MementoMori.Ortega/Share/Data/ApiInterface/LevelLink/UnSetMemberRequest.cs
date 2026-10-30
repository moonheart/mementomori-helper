using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LevelLink
{
	[MessagePackObject(true)]
	[OrtegaApi("levelLink/unSetMember", true, false)]
	public class UnSetMemberRequest : ApiRequestBase
	{
		public int LevelLinkCellNo { get; set; }
	}

	[MessagePackObject(true)]
	public class UnSetMemberResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
