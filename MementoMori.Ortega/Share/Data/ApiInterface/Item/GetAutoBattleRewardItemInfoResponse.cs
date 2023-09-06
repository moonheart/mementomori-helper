using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Item
{
	[MessagePackObject(true)]
	public class GetAutoBattleRewardItemInfoResponse : ApiResponseBase
	{
		public List<UserItem> RewardItemList { get; set; }
	}
}
