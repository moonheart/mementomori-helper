using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Character
{
	[MessagePackObject(true)]
	public class GetCharacterStoryRewardResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserItem> RewardItemList { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
