using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus
{
    [MessagePackObject(true)]
    public class GetMonthlyLoginBonusInfoResponse : ApiResponseBase
    {
        public long MonthlyLoginBonusId { get; set; }

        public int PastReceivedCount { get; set; }

        public List<int> ReceivedDailyRewardDayList { get; set; }

        public List<int> ReceivedLoginCountRewardDayList { get; set; }

        public GetMonthlyLoginBonusInfoResponse()
        {
        }
    }
}