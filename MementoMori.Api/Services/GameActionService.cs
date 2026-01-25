using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Share.Data.ApiInterface.LoginBonus;
using MementoMori.Ortega.Share.Data.ApiInterface.User;
using MementoMori.Ortega.Share.Data.ApiInterface;
using MementoMori.Api.Models;
using MementoMori.Ortega.Share.Data.ApiInterface.TradeShop;
using MementoMori.Ortega.Share.Data.ApiInterface.WeeklyTopics;
using MementoMori.Ortega.Share.Data.TradeShop;
using MementoMori.Api.Extensions;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Services;

/// <summary>
/// 游戏业务动作服务 - 处理自动化的具体事务
/// </summary>
public class GameActionService
{
    private readonly ILogger<GameActionService> _logger;
    private readonly AccountManager _accountManager;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public GameActionService(
        ILogger<GameActionService> logger,
        AccountManager accountManager,
        JobLogger jobLogger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _accountManager = accountManager;
        _jobLogger = jobLogger;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 执行所有快速动作（每日/每小时任务的核心）
    /// </summary>
    public async Task ExecuteAllQuickActionAsync(long userId)
    {
        await _jobLogger.LogAsync(userId, "开始执行全量自动化任务...");
        
        await GetLoginBonusAsync(userId);
        await AutoBuyShopItemAsync(userId);
        // TODO: 完善其他动作的移植
        // await GetAutoBattleRewardAsync(userId);
        // await BulkTransferFriendPointAsync(userId);
        // ...
        
        await _jobLogger.LogAsync(userId, "全量自动化任务执行完毕。");
    }

    /// <summary>
    /// 领取登录奖励
    /// </summary>
    public async Task GetLoginBonusAsync(long userId)
    {
        try
        {
            var context = await _accountManager.GetOrCreateAsync(userId);
            var nm = context.NetworkManager;

            await _jobLogger.LogAsync(userId, "正在检查登录奖励...");

            // 1. 获取登录奖励信息
            // var infoReq = new GetMonthlyLoginBonusInfoRequest();
            // var infoResp = await nm.SendRequest<GetMonthlyLoginBonusInfoRequest, GetMonthlyLoginBonusInfoResponse>(infoReq, false);
            //
            // if (infoResp.IsCanReward)
            // {
            //     // 2. 领取奖励
            //     var rewardReq = new RewardMonthlyLoginBonusRequest();
            //     await nm.SendRequest<RewardMonthlyLoginBonusRequest, RewardMonthlyLoginBonusResponse>(rewardReq, false);
            //     await _jobLogger.LogAsync(userId, "成功领取每日登录奖励！");
            // }
            // else
            {
                await _jobLogger.LogAsync(userId, "今日登录奖励已领取。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get login bonus for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"领取登录奖励失败: {ex.Message}", "Error");
        }
    }

    /// <summary>
    /// 自动购买商店物品
    /// </summary>
    public async Task AutoBuyShopItemAsync(long userId)
    {
        try
        {
            var context = await _accountManager.GetOrCreateAsync(userId);
            var nm = context.NetworkManager;

            // 获取配置
            using var scope = _serviceProvider.CreateScope();
            var settingService = scope.ServiceProvider.GetRequiredService<PlayerSettingService>();
            var shopConfig = await settingService.GetSettingAsync<GameConfig.ShopConfig>(userId, "ShopConfig");

            if (shopConfig == null || shopConfig.AutoBuyItems.Count == 0)
            {
                await _jobLogger.LogAsync(userId, "未配置商店自动购买项。");
                return;
            }

            await _jobLogger.LogAsync(userId, "正在检查商店物品...");

            // 1. 获取常规商店列表
            var listResp = await nm.SendRequest<GetListRequest, GetListResponse>(new GetListRequest());
            var tabInfoList = listResp.TradeShopTabInfoList ?? new List<TradeShopTabInfo>();

            // 2. 获取周活动商店
            try
            {
                var weeklyTopicInfo = await nm.SendRequest<GetWeeklyTopicsInfoRequest, GetWeeklyTopicsInfoResponse>(new GetWeeklyTopicsInfoRequest());
                if (weeklyTopicInfo?.ShopData?.TradeShopItemList != null)
                {
                    foreach (var tradeShopItems in weeklyTopicInfo.ShopData.TradeShopItemList.GroupBy(d => d.TradeShopItemId / 10000))
                    {
                        tabInfoList.Add(new TradeShopTabInfo
                        {
                            ExpirationTimeStamp = weeklyTopicInfo.ShopData.ExpirationTimeStamp,
                            TradeShopItems = tradeShopItems.ToList(),
                            TradeShopTabId = tradeShopItems.Key
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to fetch weekly topics shop for user {UserId}", userId);
            }

            foreach (var tabInfo in tabInfoList)
            {
                if (tabInfo.TradeShopItems == null) continue;

                foreach (var shopItem in tabInfo.TradeShopItems)
                {
                    if (shopItem.IsSoldOut()) continue;

                    // 匹配逻辑
                    if (!shopItem.IsFree())
                    {
                        var match = shopConfig.AutoBuyItems.FirstOrDefault(config =>
                        {
                            // 匹配 Tab
                            if (config.ShopTabId != tabInfo.TradeShopTabId) return false;

                            // 匹配物品 (如果有指定 BuyItem)
                            if (config.BuyItem != null && !shopItem.GiveItem.IsEqual(config.BuyItem)) return false;

                            // 匹配消耗 (如果有指定 ConsumeItem)
                            if (config.ConsumeItem != null &&
                                !shopItem.ConsumeItem1.IsEqual(config.ConsumeItem) &&
                                !shopItem.ConsumeItem2.IsEqual(config.ConsumeItem)) return false;

                            // 折扣检查
                            if (shopItem.SalePercent < config.MinDiscountPercent) return false;

                            return true;
                        });

                        if (match == null) continue;

                        // 余额检查
                        if (shopItem.ConsumeItem1 != null &&
                            shopItem.ConsumeItem1.ItemCount > nm.UserSyncData.GetUserItemCount(shopItem.ConsumeItem1.ItemType, shopItem.ConsumeItem1.ItemId, true))
                            continue;
                        if (shopItem.ConsumeItem2 != null &&
                            shopItem.ConsumeItem2.ItemCount > nm.UserSyncData.GetUserItemCount(shopItem.ConsumeItem2.ItemType, shopItem.ConsumeItem2.ItemId, true))
                            continue;
                    }

                    // 执行购买
                    try
                    {
                        var buyReq = new BuyItemRequest
                        {
                            TradeShopTabId = tabInfo.TradeShopTabId,
                            TradeShopItemInfos = new List<TradeShopItemInfo>
                            {
                                new TradeShopItemInfo { TradeShopItemId = shopItem.TradeShopItemId, TradeCount = 1 }
                            }
                        };

                        var buyResp = await nm.SendRequest<BuyItemRequest, BuyItemResponse>(buyReq);
                        
                        var itemName = ItemUtil.GetItemName(shopItem.GiveItem.ItemType, shopItem.GiveItem.ItemId);
                        await _jobLogger.LogAsync(userId, $"成功购买: {itemName} x {shopItem.GiveItem.ItemCount}");
                    }
                    catch (Exception ex)
                    {
                        await _jobLogger.LogAsync(userId, $"购买失败: {ex.Message}", "Error");
                    }
                }
            }

            await _jobLogger.LogAsync(userId, "商店自动购买检查完毕。");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to execute AutoBuyShopItem for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"商店自动化执行异常: {ex.Message}", "Error");
        }
    }

    // 更多动作待移植...
}
