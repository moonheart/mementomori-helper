using MementoMori.Exceptions;
using MementoMori.Ortega.Share.Data.ApiInterface.Shop;
using MementoMori.Ortega.Share.Data.ApiInterface.TradeShop;
using MementoMori.Ortega.Share.Data.ApiInterface.Vip;
using MementoMori.Ortega.Share.Data.TradeShop;
using GetListRequest = MementoMori.Ortega.Share.Data.ApiInterface.Shop.GetListRequest;
using GetListResponse = MementoMori.Ortega.Share.Data.ApiInterface.Shop.GetListResponse;

namespace MementoMori;

public partial class MementoMoriFuncs
{
    public async Task ReceiveMonthlyBoost()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var listResponse = await GetResponse<GetListRequest, GetListResponse>(new GetListRequest());
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

            var listResponse =
                await GetResponse<Ortega.Share.Data.ApiInterface.TradeShop.GetListRequest, Ortega.Share.Data.ApiInterface.TradeShop.GetListResponse>(
                    new Ortega.Share.Data.ApiInterface.TradeShop.GetListRequest());

            log(ResourceStrings.ShopAutoBuyItems);
            foreach (var tabInfo in listResponse.TradeShopTabInfoList)
            {
                if (tabInfo.TradeShopItems == null) continue;

                foreach (var shopItem in tabInfo.TradeShopItems)
                {
                    if (shopItem.IsSoldOut()) continue;

                    var shopAutoBuyItem = autoBuyItems.Find(d =>
                    {
                        if (d.ShopTabId != tabInfo.TradeShopTabId) return false;
                        if (d.BuyItem == null && d.ConsumeItem == null) return false;
                        if (d.BuyItem == null && (
                                (d.ConsumeItem.ItemType == shopItem.ConsumeItem1.ItemType && d.ConsumeItem.ItemId == shopItem.ConsumeItem1.ItemId)
                                || (shopItem.ConsumeItem2 != null && d.ConsumeItem.ItemType == shopItem.ConsumeItem2.ItemType && d.ConsumeItem.ItemId == shopItem.ConsumeItem2.ItemId)))
                            return true;
                        if (d.ConsumeItem == null && d.BuyItem.ItemType == shopItem.GiveItem.ItemType && d.BuyItem.ItemId == shopItem.GiveItem.ItemId)
                            return true;
                        if (d.BuyItem != null && d.ConsumeItem != null && d.BuyItem.IsEqual(shopItem.GiveItem.ItemType, shopItem.GiveItem.ItemId) && (
                                d.ConsumeItem.IsEqual(shopItem.ConsumeItem1.ItemType, shopItem.ConsumeItem1.ItemId)
                                || (shopItem.ConsumeItem2 != null && d.ConsumeItem.IsEqual(shopItem.ConsumeItem2.ItemType, shopItem.ConsumeItem2.ItemId))))
                            return true;

                        return false;
                    });

                    if (shopAutoBuyItem == null) continue;

                    if (shopItem.ConsumeItem1.ItemCount > UserSyncData.UserItemDtoInfo.GetCount(shopItem.ConsumeItem1.ItemType, shopItem.ConsumeItem1.ItemId))
                        continue;
                    if (shopItem.ConsumeItem2 != null && shopItem.ConsumeItem2.ItemCount > UserSyncData.UserItemDtoInfo.GetCount(shopItem.ConsumeItem2.ItemType, shopItem.ConsumeItem2.ItemId))
                        continue;

                    if (shopItem.SalePercent < shopAutoBuyItem.MinDiscountPercent) continue;

                    try
                    {
                        var response = await GetResponse<BuyItemRequest, BuyItemResponse>(
                            new BuyItemRequest
                            {
                                TradeShopTabId = tabInfo.TradeShopTabId, TradeShopItemInfos = new List<TradeShopItemInfo> {new() {TradeShopItemId = shopItem.TradeShopItemId, TradeCount = 1}}
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

    private async Task<bool> IsValidMonthlyBoost()
    {
        return UserSyncData.UserShopMonthlyBoostDtoInfos.Exists(d => d.ExpirationTimeStamp > DateTimeOffset.Now.ToUnixTimeMilliseconds());
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