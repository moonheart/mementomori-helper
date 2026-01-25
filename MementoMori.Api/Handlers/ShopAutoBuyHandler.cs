using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.ApiInterface.TradeShop;
using MementoMori.Ortega.Share.Data.ApiInterface.WeeklyTopics;
using MementoMori.Ortega.Share.Data.TradeShop;
using MementoMori.Api.Extensions;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 商店自动购买处理器
/// </summary>
public class ShopAutoBuyHandler : IGameActionHandler
{
    private readonly ILogger<ShopAutoBuyHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;

    public ShopAutoBuyHandler(
        ILogger<ShopAutoBuyHandler> logger, 
        JobLogger jobLogger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _jobLogger = jobLogger;
        _serviceProvider = serviceProvider;
    }

    public string ActionName => "商店自动购买";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
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

        // 2. 获取周活动商店 (Try-Catch 保护)
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
                        if (config.ShopTabId != tabInfo.TradeShopTabId) return false;
                        if (config.BuyItem != null && !shopItem.GiveItem.IsEqual(config.BuyItem)) return false;
                        if (config.ConsumeItem != null &&
                            !shopItem.ConsumeItem1.IsEqual(config.ConsumeItem) &&
                            !shopItem.ConsumeItem2.IsEqual(config.ConsumeItem)) return false;
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

                    await nm.SendRequest<BuyItemRequest, BuyItemResponse>(buyReq);
                    
                    var itemName = ItemUtil.GetItemName(shopItem.GiveItem.ItemType, shopItem.GiveItem.ItemId);
                    await _jobLogger.LogAsync(userId, $"成功购买: {itemName} x {shopItem.GiveItem.ItemCount}");
                }
                catch (Exception ex)
                {
                    await _jobLogger.LogAsync(userId, $"购买失败 ({shopItem.TradeShopItemId}): {ex.Message}", "Error");
                }
            }
        }

        await _jobLogger.LogAsync(userId, "商店自动购买检查完毕。");
    }
}
