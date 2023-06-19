using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
    [MessagePackObject(true)]
    public class GetLimitedLoginBonusInfoResponse : ApiResponseBase
    {
        public bool IsReceivedSpecialReward { get; set; }

        public List<int> ReceivedDateList { get; set; }

        public int TotalLoginCount { get; set; }

        public GetLimitedLoginBonusInfoResponse()
        {
        }
    }
}