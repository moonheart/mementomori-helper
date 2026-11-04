using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LevelLink
{
	[MessagePackObject(true)]
	[OrtegaApi("levelLink/setMember", true, false)]
	public class SetMemberRequest : ApiRequestBase
	{
		public int LevelLinkCellNo { get; set; }
		public string UserCharacterGuid { get; set; }
	}

	[MessagePackObject(true)]
	public class SetMemberResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
