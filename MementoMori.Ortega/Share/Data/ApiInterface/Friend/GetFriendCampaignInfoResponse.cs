using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	public class GetFriendCampaignInfoResponse : ApiResponseBase
	{
		public string FriendCode { get; set; }

		public bool IsUsedFriendCode { get; set; }

		public List<UserFriendMissionDtoInfo> UserFriendMissionDtoInfoList { get; set; }
	}
}
