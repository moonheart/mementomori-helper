using MementoMori.Exceptions;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.ApiInterface.Item;
using MementoMori.Ortega.Share.Data.ApiInterface.Present;
using MementoMori.Ortega.Share.Data.Item.Model;

namespace MementoMori.Funcs;

public partial class MementoMoriFuncs
{
    private const double UseOverLimitItemRate = 0.3;

    public async Task PresentReceiveItem()
    {
        await ExecuteQuickAction(async (log, token) =>
        {
            var getListResponse = await GetResponse<GetListRequest, GetListResponse>(new GetListRequest {LanguageType = LanguageType.zhTW});
            if (getListResponse.userPresentDtoInfos.Any(d => !d.IsReceived))
            {
                // use over limit items before receiving
                // only QuestQuickTicket and Equipment are considered
                foreach (var presentItem in getListResponse.userPresentDtoInfos.SelectMany(d => d.ItemList).GroupBy(d => new {d.Item.ItemType, d.Item.ItemId}))
                {
                    if (presentItem.Key.ItemType == ItemType.QuestQuickTicket)
                    {
                        var count = UserSyncData.GetUserItemCount(presentItem.Key.ItemType, presentItem.Key.ItemId);
                        var itemMb = ItemTable.GetByItemTypeAndItemId(presentItem.Key.ItemType, presentItem.Key.ItemId);
                        var maxItemCount = itemMb.MaxItemCount;
                        if (count < maxItemCount) continue;

                        var name = TextResourceTable.Get(itemMb.NameKey);
                        var useCount = (int) Math.Floor(maxItemCount * UseOverLimitItemRate);
                        log($"{ResourceStrings.UseOverLimitItem}: {name}×{useCount}, {count}/{maxItemCount}");
                        var response = await GetResponse<UseAutoBattleRewardItemRequest, UseAutoBattleRewardItemResponse>(new UseAutoBattleRewardItemRequest
                        {
                            ItemType = (QuestQuickTicketType) itemMb.ItemId,
                            UseCount = useCount
                        });
                        response.RewardItemList.PrintUserItems(log);
                    }
                    else if (presentItem.Key.ItemType == ItemType.Equipment)
                    {
                        var count = UserSyncData.GetUserItemCount(presentItem.Key.ItemType, presentItem.Key.ItemId);
                        var maxItemCount = 999;
                        if (count < maxItemCount) continue;

                        var name = TextResourceTable.Get(EquipmentTable.GetById(presentItem.Key.ItemId).NameKey);
                        var useCount = (int) Math.Floor(maxItemCount * UseOverLimitItemRate);
                        log($"{ResourceStrings.UseOverLimitItem}: {name}×{useCount}, {count}/{maxItemCount}");
                        var response = await GetResponse<CastRequest, CastResponse>(new CastRequest {UserEquipment = new UserEquipment(presentItem.Key.ItemId, useCount)});
                        response.ResultItemList.PrintUserItems(log);
                    }
                }

                try
                {
                    var resp = await GetResponse<ReceiveItemRequest, ReceiveItemResponse>(new ReceiveItemRequest {LanguageType = NetworkManager.LanguageType});
                    log($"{TextResourceTable.Get("[MyPagePresentBoxButtonTitle]")} {TextResourceTable.Get("[MyPagePresentBoxButtonAllReceive]")}");
                    resp.ResultItems.Select(d => d.Item).PrintUserItems(log);
                }
                catch (ApiErrorException e) when (e.ErrorCode == ErrorCode.PresentReceiveOverLimitCountPresent)
                {
                    log(e.Message);
                    // receive one by one
                    foreach (var present in getListResponse.userPresentDtoInfos.Where(d => !d.IsReceived))
                    {
                        try
                        {
                            var resp = await GetResponse<ReceiveItemRequest, ReceiveItemResponse>(
                                new ReceiveItemRequest {LanguageType = NetworkManager.LanguageType, PresentGuid = present.Guid});
                            resp.ResultItems.Select(d => d.Item).PrintUserItems(log);
                        }
                        catch (ApiErrorException e2) when (e2.ErrorCode == ErrorCode.PresentReceiveOverLimitCountPresent)
                        {
                            // ignored
                        }
                    }
                }
            }
            else
                log(TextResourceTable.GetErrorCodeMessage(ErrorCode.PresentReceiveAlreadyReceivedPresent));
        });
    }
}