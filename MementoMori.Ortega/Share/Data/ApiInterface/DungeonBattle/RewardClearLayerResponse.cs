using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
	[MessagePackObject(true)]
	public class RewardClearLayerResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<UserItem> RewardItems
		{
			get;
			set;
		}

		public UserSyncData UserSyncData
		{
			get;
			set;
		}

		public RewardClearLayerResponse()
		{
		}
	}
}
