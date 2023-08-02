using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend
{
	[MessagePackObject(true)]
	public class UseFriendCodeResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserItem> RewardItemList { get; set; }
		public UserSyncData UserSyncData{ get; set; }
	}
}
