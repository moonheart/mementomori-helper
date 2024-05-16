using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	public class ReceiveFloorRewardResponse : ApiResponseBase, IGuildSyncApiResponse, IUserSyncApiResponse
	{
		public List<UserItem> RewardItemList { get; set; }

		public GuildSyncData GuildSyncData { get; set; }

		public UserSyncData UserSyncData { get; set; }
	}
}
