using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
    [MessagePackObject(true)]
    public class ReceiveDailyLoginBonusResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public List<UserItem> RewardItemList { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public ReceiveDailyLoginBonusResponse()
        {
        }
    }
}