using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
	[MessagePackObject(true)]
	public class ReceiveSpecialLimitedLoginBonusResponse : ApiResponseBase, IUserSyncApiResponse
	{
		public List<LimitedLoginBonusRewardItem> RewardItemList{ get; set; }

		public UserSyncData UserSyncData{ get; set; }

		public ReceiveSpecialLimitedLoginBonusResponse()
		{
		}
	}
}
