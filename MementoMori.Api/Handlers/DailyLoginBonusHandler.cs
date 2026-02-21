using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 登录奖励领取处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class DailyLoginBonusHandler : IGameActionHandler
{
    private readonly ILogger<DailyLoginBonusHandler> _logger;
    private readonly JobLogger _jobLogger;

    public string ActionName => "领取登录奖励";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;
        var timeManager = context.TimeManager;

        await _jobLogger.LogAsync(userId, "正在检查登录奖励...");

        try
        {
            // 1. 获取月度登录奖励信息
            var monthlyLoginBonusInfo = await GetMonthlyLoginBonusInfoAsync(nm);

            // 2. 领取每日登录奖励
            await ClaimDailyLoginBonusAsync(userId, nm, timeManager, monthlyLoginBonusInfo);

            // 3. 领取月度登录次数奖励
            await ClaimLoginCountBonusAsync(userId, nm, monthlyLoginBonusInfo);

            // 4. 刷新月度登录奖励信息
            monthlyLoginBonusInfo = await GetMonthlyLoginBonusInfoAsync(nm);

            // 5. 领取限时登录奖励
            await ClaimLimitedLoginBonusAsync(userId, nm);

            await _jobLogger.LogAsync(userId, "登录奖励检查/领取完毕。");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "检查登录奖励失败 for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"登录奖励处理出错: {ex.Message}");
        }
    }

    /// <summary>
    /// 获取月度登录奖励信息
    /// </summary>
    private async Task<GetMonthlyLoginBonusInfoResponse> GetMonthlyLoginBonusInfoAsync(NetworkManager nm)
    {
        return await nm.SendRequest<GetMonthlyLoginBonusInfoRequest, GetMonthlyLoginBonusInfoResponse>(
            new GetMonthlyLoginBonusInfoRequest());
    }

    /// <summary>
    /// 领取每日登录奖励
    /// </summary>
    private async Task ClaimDailyLoginBonusAsync(long userId, NetworkManager nm, TimeManager timeManager,
        GetMonthlyLoginBonusInfoResponse monthlyLoginBonusInfo)
    {
        var day = timeManager.ServerNow.Day;

        if (monthlyLoginBonusInfo.ReceivedDailyRewardDayList.Contains(day))
        {
            await _jobLogger.LogAsync(userId,
                Masters.TextResourceTable.GetErrorCodeMessage(ErrorCode.LoginBonusAlreadyReceivedDailyReward));
            return;
        }

        // 检查是否已过 4:00（服务器时间）
        if (timeManager.ServerNow.Hour >= 4)
        {
            try
            {
                var bonus = await nm.SendRequest<ReceiveDailyLoginBonusRequest, ReceiveDailyLoginBonusResponse>(
                    new ReceiveDailyLoginBonusRequest { ReceiveDay = timeManager.ServerNow.Day });

                await _jobLogger.LogAsync(userId,
                    $"{Masters.TextResourceTable.Get("[MyPageButtonLoginBonusLabel]")}:");
                await LogUserItemsAsync(userId, bonus.RewardItemList);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "领取每日登录奖励失败 for user {UserId}", userId);
            }
        }
    }

    /// <summary>
    /// 领取月度登录次数奖励
    /// </summary>
    private async Task ClaimLoginCountBonusAsync(long userId, NetworkManager nm,
        GetMonthlyLoginBonusInfoResponse monthlyLoginBonusInfo)
    {
        var monthlyLoginBonusMb = Masters.MonthlyLoginBonusTable.GetById(monthlyLoginBonusInfo.MonthlyLoginBonusId);
        if (monthlyLoginBonusMb == null)
        {
            _logger.LogWarning("MonthlyLoginBonusMB not found for id {Id}", monthlyLoginBonusInfo.MonthlyLoginBonusId);
            return;
        }

        var monthlyLoginBonusRewardListMb =
            Masters.MonthlyLoginBonusRewardListTable.GetById(monthlyLoginBonusMb.RewardListId);
        if (monthlyLoginBonusRewardListMb?.LoginCountRewardList == null) return;

        foreach (var loginCountRewardInfo in monthlyLoginBonusRewardListMb.LoginCountRewardList)
        {
            // 登录次数达到 && 没有领取过
            if (loginCountRewardInfo.DayCount <= monthlyLoginBonusInfo.ReceivedDailyRewardDayList.Count &&
                !monthlyLoginBonusInfo.ReceivedLoginCountRewardDayList.Contains(loginCountRewardInfo.DayCount))
            {
                try
                {
                    var resp = await nm.SendRequest<ReceiveLoginCountBonusRequest, ReceiveLoginCountBonusResponse>(
                        new ReceiveLoginCountBonusRequest { ReceiveDayCount = loginCountRewardInfo.DayCount });

                    await _jobLogger.LogAsync(userId,
                        $"{Masters.TextResourceTable.Get("[LoginBonusCountFormat]", loginCountRewardInfo.DayCount, 30)}");
                    await LogUserItemsAsync(userId, resp.RewardItemList);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "领取月度登录次数奖励失败 for user {UserId}, dayCount {DayCount}", userId,
                        loginCountRewardInfo.DayCount);
                }
            }
        }
    }

    /// <summary>
    /// 领取限时登录奖励
    /// </summary>
    private async Task ClaimLimitedLoginBonusAsync(long userId, NetworkManager nm)
    {
        // 获取 Mypage 信息以获取限时登录奖励 ID
        GetMypageResponse mypageResponse;
        try
        {
            mypageResponse = await nm.SendRequest<GetMypageRequest, GetMypageResponse>(
                new GetMypageRequest { LanguageType = nm.LanguageType });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "获取 Mypage 信息失败 for user {UserId}", userId);
            return;
        }

        var iconInfo = mypageResponse.MypageInfo?.MypageIconInfos?
            .FirstOrDefault(d => d.TransferDetailInfo?.TransferSpotType == TransferSpotType.LimitedLoginBonus);

        if (iconInfo == null) return;

        var limitedLoginBonusId = iconInfo.TransferDetailInfo.NumberInfo1;

        GetLimitedLoginBonusInfoResponse limitedLoginBonusInfo;
        try
        {
            limitedLoginBonusInfo =
                await nm.SendRequest<GetLimitedLoginBonusInfoRequest, GetLimitedLoginBonusInfoResponse>(
                    new GetLimitedLoginBonusInfoRequest { LimitedLoginBonusId = limitedLoginBonusId });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "获取限时登录奖励信息失败 for user {UserId}, limitedLoginBonusId {Id}", userId,
                limitedLoginBonusId);
            return;
        }

        var limitedLoginBonusMb = Masters.LimitedLoginBonusTable.GetById(limitedLoginBonusId);
        if (limitedLoginBonusMb == null)
        {
            _logger.LogWarning("LimitedLoginBonusMB not found for id {Id}", limitedLoginBonusId);
            return;
        }

        var name = Masters.TextResourceTable.Get(limitedLoginBonusMb.TitleTextKey);
        var loginBonusRewardListMb = Masters.LimitedLoginBonusRewardListTable.GetById(limitedLoginBonusId);

        if (loginBonusRewardListMb?.DailyRewardList == null) return;

        // 领取每日限时登录奖励
        foreach (var dailyLimitedLoginBonusItem in loginBonusRewardListMb.DailyRewardList)
        {
            if (limitedLoginBonusInfo.ReceivedDateList.Contains(dailyLimitedLoginBonusItem.Date)) continue;
            if (limitedLoginBonusInfo.TotalLoginCount < dailyLimitedLoginBonusItem.Date) continue;

            try
            {
                await _jobLogger.LogAsync(userId, name);
                var resp =
                    await nm.SendRequest<ReceiveDailyLimitedLoginBonusRequest, ReceiveDailyLimitedLoginBonusResponse>(
                        new ReceiveDailyLimitedLoginBonusRequest
                        {
                            LimitedLoginBonusId = limitedLoginBonusId,
                            ReceiveDate = dailyLimitedLoginBonusItem.Date
                        });
                await LogUserItemsAsync(userId, resp.RewardItemList);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "领取限时登录奖励失败 for user {UserId}, date {Date}", userId,
                    dailyLimitedLoginBonusItem.Date);
            }
        }

        // 领取特殊限时登录奖励
        if (loginBonusRewardListMb.ExistSpecialReward &&
            limitedLoginBonusInfo.TotalLoginCount >= loginBonusRewardListMb.SpecialRewardItem.Date &&
            !limitedLoginBonusInfo.IsReceivedSpecialReward)
        {
            try
            {
                await _jobLogger.LogAsync(userId, name);
                var response =
                    await nm.SendRequest<ReceiveSpecialLimitedLoginBonusRequest,
                        ReceiveSpecialLimitedLoginBonusResponse>(
                        new ReceiveSpecialLimitedLoginBonusRequest { LimitedLoginBonusId = limitedLoginBonusId });
                await LogUserItemsAsync(userId, response.RewardItemList);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "领取特殊限时登录奖励失败 for user {UserId}", userId);
            }
        }
    }

    /// <summary>
    /// 输出用户道具列表到日志
    /// </summary>
    private async Task LogUserItemsAsync(long userId, IEnumerable<IUserItem> userItems)
    {
        if (userItems == null) return;

        foreach (var userItem in userItems)
        {
            var itemName = ItemUtil.GetItemName(userItem.ItemType, userItem.ItemId);
            var itemRarity = ItemUtil.GetItemRarity(userItem.ItemType, userItem.ItemId);
            await _jobLogger.LogAsync(userId, $"  {itemName}({itemRarity}) × {userItem.ItemCount}");
        }
    }

    /// <summary>
    /// 输出用户角色道具列表到日志
    /// </summary>
    private async Task LogUserItemsAsync(long userId, IEnumerable<IUserCharacterItem> userItems)
    {
        if (userItems == null) return;

        foreach (var userItem in userItems)
        {
            var item = userItem.Item;
            var itemName = ItemUtil.GetItemName(item.ItemType, item.ItemId);
            var itemRarity = ItemUtil.GetItemRarity(item.ItemType, item.ItemId);
            await _jobLogger.LogAsync(userId, $"  {itemName}({itemRarity}) × {item.ItemCount}");
        }
    }
}
