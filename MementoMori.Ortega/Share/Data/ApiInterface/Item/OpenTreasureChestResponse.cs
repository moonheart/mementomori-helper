using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.TreasureChest;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Item
{
	[MessagePackObject(true)]
	public class OpenTreasureChestResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<TreasureChestReward> RewardItems { get; set; }

		public UserSyncData UserSyncData{ get; set; }
	}
}
