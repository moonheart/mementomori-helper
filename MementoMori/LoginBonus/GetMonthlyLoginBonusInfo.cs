namespace MementoMori.LoginBonus;

public class GetMonthlyLoginBonusInfo
{
    public class Req
    {
    }

    public class Resp
    {
        public int MonthlyLoginBonusId { get; set; }
        public int PastReceivedCount { get; set; }
        public int[] ReceivedDailyRewardDayList { get; set; }
        public int[] ReceivedLoginCountRewardDayList { get; set; }
    }
}