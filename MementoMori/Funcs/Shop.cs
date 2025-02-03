using MementoMori.Exceptions;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.Shop;
using MementoMori.Ortega.Share.Data.ApiInterface.TradeShop;
using MementoMori.Ortega.Share.Data.ApiInterface.Vip;
using MementoMori.Ortega.Share.Data.ApiInterface.WeeklyTopics;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.TradeShop;
using ShopGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.Shop.GetListRequest;
using ShopGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.Shop.GetListResponse;
using TradeShopGetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.TradeShop.GetListRequest;
using TradeShopGetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.TradeShop.GetListResponse;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    public async Task ReceiveMonthlyBoost()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var listResponse = await GetResponse<ShopGetListRequest, ShopGetListResponse>(new ShopGetListRequest());
            var shopProductInfo = listResponse.ShopTabInfoList.SelectMany(d => d.ShopProductInfoList).FirstOrDefault(d => d.ShopProductType == ShopProductType.MonthlyBoost);
            if (shopProductInfo != null && shopProductInfo.ShopProductMonthlyBoost.ExpirationTimeStamp >= DateTimeOffset.Now.ToUnixTimeMilliseconds())
            {
                if (shopProductInfo.ShopProductMonthlyBoost.IsAlreadyReceive)
                    log($"{TextResourceTable.Get("[CommonMonthlyBoosterLabel]")} {TextResourceTable.Get("[ShopMonthlyBoostRewardDetailReceivedMessage]")}");
                else
                {
                    var receiveRewardResponse = await GetResponse<ReceiveRewardRequest, ReceiveRewardResponse>(new ReceiveRewardRequest
                        {MBId = shopProductInfo.MbId, ProductId = shopProductInfo.ShopProductMonthlyBoost.ProductId, ShopProductType = ShopProductType.MonthlyBoost});
                    log($"{TextResourceTable.Get("[CommonMonthlyBoosterLabel]")} {TextResourceTable.Get("[ShopMonthlyBoostRewardDetailReceivedMessage]")}");
                    receiveRewardResponse.RewardInfo.ItemList.PrintUserItems(log);
                    receiveRewardResponse.RewardInfo.BonusItemList.PrintUserItems(log);
                    receiveRewardResponse.RewardInfo.CharacterList.PrintCharacterDtos(log);
                }
            }
        });
    }

    public async Task AutoBuyShopItem()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var autoBuyItems = GameConfig.Shop.AutoBuyItems;
            if (autoBuyItems.Count == 0) return;

            var listResponse = await GetResponse<TradeShopGetListRequest, TradeShopGetListResponse>(new TradeShopGetListRequest());

            log(ResourceStrings.ShopAutoBuyItems);
            var tabInfoList = listResponse.TradeShopTabInfoList;

            var weeklyTopicInfo = await GetResponse<GetWeeklyTopicsInfoRequest, GetWeeklyTopicsInfoResponse>(new GetWeeklyTopicsInfoRequest());
            foreach (var tradeShopItems in weeklyTopicInfo.ShopData.TradeShopItemList.GroupBy(d => d.TradeShopItemId / 10000))
            {
                tabInfoList.Add(new TradeShopTabInfo
                {
                    ExpirationTimeStamp = weeklyTopicInfo.ShopData.ExpirationTimeStamp,
                    TradeShopItems = tradeShopItems.ToList(),
                    TradeShopTabId = tradeShopItems.Key
                });
            }

            foreach (var tabInfo in tabInfoList)
            {
                if (tabInfo.TradeShopItems == null) continue;

                foreach (var shopItem in tabInfo.TradeShopItems)
                {
                    if (shopItem.IsSoldOut()) continue;

                    if (!shopItem.IsFree())
                    {
                        var shopAutoBuyItem = autoBuyItems.Find(autoBuyItem =>
                        {
                            // skip if not match tab
                            if (autoBuyItem.ShopTabId != tabInfo.TradeShopTabId) return false;

                            // skip if not match item
                            if (autoBuyItem.BuyItem == null && autoBuyItem.ConsumeItem == null) return false;

                            // if buy item is not specified and consume item is matched, buy it
                            if (autoBuyItem.BuyItem == null && (autoBuyItem.ConsumeItem.IsConsumeEqual(shopItem.ConsumeItem1) || autoBuyItem.ConsumeItem.IsConsumeEqual(shopItem.ConsumeItem2)))
                                return true;

                            // if buy item is matched and consume item is not specified, buy it
                            if (autoBuyItem.ConsumeItem == null && shopItem.GiveItem.IsEqual(autoBuyItem.BuyItem))
                                return true;

                            // if both buy item and consume item are matched, buy it
                            if (autoBuyItem.BuyItem != null
                                && autoBuyItem.ConsumeItem != null
                                && autoBuyItem.BuyItem.IsEqual(shopItem.GiveItem)
                                && (autoBuyItem.ConsumeItem.IsConsumeEqual(shopItem.ConsumeItem1) || autoBuyItem.ConsumeItem.IsConsumeEqual(shopItem.ConsumeItem2)))
                                return true;

                            // otherwise, skip
                            return false;
                        });

                        if (shopAutoBuyItem == null) continue;

                        // check if user has enough items to consume
                        if (shopItem.ConsumeItem1 != null && shopItem.ConsumeItem1.ItemCount > UserSyncData.GetUserItemCount(shopItem.ConsumeItem1.ItemType, shopItem.ConsumeItem1.ItemId, true))
                            continue;
                        if (shopItem.ConsumeItem2 != null && shopItem.ConsumeItem2.ItemCount > UserSyncData.GetUserItemCount(shopItem.ConsumeItem2.ItemType, shopItem.ConsumeItem2.ItemId, true))
                            continue;

                        // check if discount is enough
                        if (shopItem.SalePercent < shopAutoBuyItem.MinDiscountPercent) continue;
                    }

                    try
                    {
                        // buy item
                        var response = await GetResponse<BuyItemRequest, BuyItemResponse>(
                            new BuyItemRequest
                            {
                                TradeShopTabId = tabInfo.TradeShopTabId, TradeShopItemInfos = [new TradeShopItemInfo {TradeShopItemId = shopItem.TradeShopItemId, TradeCount = 1}]
                            });
                        response.UserSyncData.GivenItemCountInfoList.PrintUserItems(log);
                    }
                    catch (ApiErrorException e)
                    {
                        log(e.Message);
                    }
                }
            }

            log($"{ResourceStrings.ShopAutoBuyItems} {ResourceStrings.Finished}");
        });
    }

    private Task<bool> IsValidMonthlyBoost()
    {
        return Task.FromResult(UserSyncData.UserShopMonthlyBoostDtoInfos.Exists(d => d.ExpirationTimeStamp > DateTimeOffset.Now.ToUnixTimeMilliseconds()));
        // return false;
    }

    public async Task GetVipGift()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            if (UserSyncData.ExistVipDailyGift == true)
            {
                var bonus = await GetResponse<GetDailyGiftRequest, GetDailyGiftResponse>(new GetDailyGiftRequest());
                log($"{TextResourceTable.Get("[VipDailyRewardLabelFormat]", UserSyncData.UserStatusDtoInfo.Vip)}\n");
                bonus.ItemList.PrintUserItems(log);
            }
            else
                log($"{TextResourceTable.GetErrorCodeMessage(ErrorCode.VipGetDailyGiftAlreadyGet)}");
        });
    }
}

public static class ShopFuncExtensions
{
    public static bool IsConsumeEqual(this UserItem thisItem, IUserItem thatItem)
    {
        if (thisItem == null) return false;
        if (thatItem == null) return false;
        if (!thisItem.IsEqual(thatItem.ItemType, thatItem.ItemId)) return false;
        if (thisItem.ItemCount == 0 || thatItem.ItemCount == 0) return true;
        return thisItem.ItemCount == thatItem.ItemCount;
    }
}