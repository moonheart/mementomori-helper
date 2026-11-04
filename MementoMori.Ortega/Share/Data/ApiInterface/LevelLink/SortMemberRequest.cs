using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LevelLink
{
	[MessagePackObject(true)]
	[OrtegaApi("levelLink/sortMember", true, false)]
	public class SortMemberRequest : ApiRequestBase
	{
		public CharacterSortType CharacterSortType { get; set; }
		public bool IsAscending { get; set; }
	}

	[MessagePackObject(true)]
	public class SortMemberResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public UserSyncData UserSyncData { get; set; }
	}
}
