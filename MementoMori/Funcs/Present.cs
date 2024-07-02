using MementoMori.Common.Localization;
using MementoMori.Exceptions;
using MementoMori.Extensions;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.ApiInterface.Item;
using MementoMori.Ortega.Share.Data.ApiInterface.Present;
using MementoMori.Ortega.Share.Data.Item.Model;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori;

public partial class MementoMoriFuncs
{
    public async Task PresentReceiveItem()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var usedItem = false;
            do
            {
                var getListResponse = await GetResponse<GetListRequest, GetListResponse>(new GetListRequest {LanguageType = LanguageType.zhTW});
                if (getListResponse.userPresentDtoInfos.Any(d => !d.IsReceived))
                    try
                    {
                        var resp = await GetResponse<ReceiveItemRequest, ReceiveItemResponse>(new ReceiveItemRequest() {LanguageType = LanguageType.zhTW});
                        usedItem = false;
                        log($"{Masters.TextResourceTable.Get("[MyPagePresentBoxButtonTitle]")} {Masters.TextResourceTable.Get("[MyPagePresentBoxButtonAllReceive]")}");
                        resp.ResultItems.Select(d => d.Item).PrintUserItems(log);
                    }
                    catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.PresentReceiveOverLimitCountPresent)
                    {
                        log(e.Message);
                        var grp = getListResponse.userPresentDtoInfos.SelectMany(d => d.ItemList).GroupBy(d => new {d.Item.ItemType, d.Item.ItemId});
                        if (grp.Count(d => d.Key.ItemType != ItemType.ExchangePlaceItem) == 0)
                        {
                            break;
                        }

                        foreach (var presentItem in grp)
                        {
                            if (presentItem.Key.ItemType == ItemType.QuestQuickTicket)
                            {
                                var count = UserSyncData.GetUserItemCount(presentItem.Key.ItemType, presentItem.Key.ItemId);
                                var itemMb = Masters.ItemTable.GetByItemTypeAndItemId(presentItem.Key.ItemType, presentItem.Key.ItemId);
                                var maxItemCount = itemMb.MaxItemCount;
                                if (count < maxItemCount) continue;

                                var name = Masters.TextResourceTable.Get(itemMb.NameKey);
                                var useCount = (int) Math.Floor(maxItemCount * 0.1);
                                log($"{ResourceStrings.UseOverLimitItem}: {name}×{useCount}, {count}/{maxItemCount}");
                                var response = await GetResponse<UseAutoBattleRewardItemRequest, UseAutoBattleRewardItemResponse>(new UseAutoBattleRewardItemRequest()
                                {
                                    ItemType = (QuestQuickTicketType) (itemMb.ItemId),
                                    UseCount = useCount
                                });
                                response.RewardItemList.PrintUserItems(log);
                                usedItem = true;
                                break;
                            }

                            if (presentItem.Key.ItemType == ItemType.Equipment)
                            {
                                var count = UserSyncData.GetUserItemCount(presentItem.Key.ItemType, presentItem.Key.ItemId);
                                var maxItemCount = 999;
                                if (count < maxItemCount) continue;

                                var name = Masters.TextResourceTable.Get(Masters.EquipmentTable.GetById(presentItem.Key.ItemId).NameKey);
                                var useCount = (int) Math.Floor(maxItemCount * 0.1);
                                log($"{ResourceStrings.UseOverLimitItem}: {name}×{useCount}, {count}/{maxItemCount}");
                                var response = await GetResponse<CastRequest, CastResponse>(new CastRequest {UserEquipment = new UserEquipment(presentItem.Key.ItemId, useCount)});
                                response.ResultItemList.PrintUserItems(log);
                                usedItem = true;
                                break;
                            }
                        }
                    }
                else
                    log(Masters.TextResourceTable.GetErrorCodeMessage(ErrorCode.PresentReceiveAlreadyReceivedPresent));
            } while (usedItem);
        });
    }
}