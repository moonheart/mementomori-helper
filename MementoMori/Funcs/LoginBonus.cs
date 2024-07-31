using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task GetLoginBonus()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            await GetMonthlyLoginBonusInfo();
            var day = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(1)).Day;
            if (!MonthlyLoginBonusInfo.ReceivedDailyRewardDayList.Contains(day))
            {
                if (TimeManager.ServerNow.Hour >= 4)
                {
                    var bonus = await GetResponse<ReceiveDailyLoginBonusRequest, ReceiveDailyLoginBonusResponse>(new ReceiveDailyLoginBonusRequest {ReceiveDay = TimeManager.ServerNow.Day});
                    log($"{TextResourceTable.Get("[MyPageButtonLoginBonusLabel]")}:\n");
                    bonus.RewardItemList.PrintUserItems(log);
                }

                await GetMonthlyLoginBonusInfo();
            }
            else
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.LoginBonusAlreadyReceivedDailyReward));

            var monthlyLoginBonusMb = MonthlyLoginBonusTable.GetById(MonthlyLoginBonusInfo.MonthlyLoginBonusId);
            var monthlyLoginBonusRewardListMb = MonthlyLoginBonusRewardListTable.GetById(monthlyLoginBonusMb.RewardListId);
            foreach (var loginCountRewardInfo in monthlyLoginBonusRewardListMb.LoginCountRewardList)
                // 登录次数达到 && 没有领取过
            {
                if (loginCountRewardInfo.DayCount <= MonthlyLoginBonusInfo.ReceivedDailyRewardDayList.Count &&
                    !MonthlyLoginBonusInfo.ReceivedLoginCountRewardDayList.Contains(loginCountRewardInfo.DayCount))
                {
                    var resp = await GetResponse<ReceiveLoginCountBonusRequest, ReceiveLoginCountBonusResponse>(new ReceiveLoginCountBonusRequest
                    {
                        ReceiveDayCount = loginCountRewardInfo.DayCount
                    });
                    log($"{TextResourceTable.Get("[LoginBonusCountFormat]", loginCountRewardInfo.DayCount, 30)}");
                    resp.RewardItemList.PrintUserItems(log);
                }
            }

            await GetMonthlyLoginBonusInfo();

            var iconInfo = Mypage.MypageInfo.MypageIconInfos.FirstOrDefault(d => d.TransferDetailInfo.TransferSpotType == TransferSpotType.LimitedLoginBonus);
            if (iconInfo != null)
            {
                var limitedLoginBonusId = iconInfo.TransferDetailInfo.NumberInfo1;
                var limitedLoginBonusInfoResponse = await GetResponse<GetLimitedLoginBonusInfoRequest, GetLimitedLoginBonusInfoResponse>(new GetLimitedLoginBonusInfoRequest
                {
                    LimitedLoginBonusId = limitedLoginBonusId
                });

                var limitedLoginBonusMb = LimitedLoginBonusTable.GetById(limitedLoginBonusId);
                var name = TextResourceTable.Get(limitedLoginBonusMb.TitleTextKey);
                var loginBonusRewardListMb = LimitedLoginBonusRewardListTable.GetById(limitedLoginBonusId);
                foreach (var dailyLimitedLoginBonusItem in loginBonusRewardListMb.DailyRewardList)
                {
                    if (limitedLoginBonusInfoResponse.ReceivedDateList.Contains(dailyLimitedLoginBonusItem.Date)) continue;
                    if (limitedLoginBonusInfoResponse.TotalLoginCount < dailyLimitedLoginBonusItem.Date) continue;
                    log(name);
                    var resp = await GetResponse<ReceiveDailyLimitedLoginBonusRequest, ReceiveDailyLimitedLoginBonusResponse>(new ReceiveDailyLimitedLoginBonusRequest
                    {
                        LimitedLoginBonusId = limitedLoginBonusId, ReceiveDate = dailyLimitedLoginBonusItem.Date
                    });
                    resp.RewardItemList.PrintUserItems(log);
                }

                if (loginBonusRewardListMb.ExistSpecialReward && limitedLoginBonusInfoResponse.TotalLoginCount >= loginBonusRewardListMb.SpecialRewardItem.Date &&
                    !limitedLoginBonusInfoResponse.IsReceivedSpecialReward)
                {
                    log(name);
                    var response = await GetResponse<ReceiveSpecialLimitedLoginBonusRequest, ReceiveSpecialLimitedLoginBonusResponse>(new ReceiveSpecialLimitedLoginBonusRequest
                    {
                        LimitedLoginBonusId = limitedLoginBonusId
                    });
                    response.RewardItemList.PrintUserItems(log);
                }
            }
        });
    }

    public async Task GetMonthlyLoginBonusInfo()
    {
        var response = await GetResponse<GetMonthlyLoginBonusInfoRequest, GetMonthlyLoginBonusInfoResponse>(new GetMonthlyLoginBonusInfoRequest());
        MonthlyLoginBonusInfo = response;
    }
}