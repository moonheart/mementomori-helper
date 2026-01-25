using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Api.Extensions;

public static class UserSyncDataExtensions
{
    public static long GetUserItemCount(this UserSyncData usersyncData, ItemType itemType, long itemId = 0, bool isAnyCurrency = false)
    {
        if (usersyncData?.UserItemDtoInfo == null) return 0;
        
        if (isAnyCurrency && (itemType == ItemType.CurrencyFree || itemType == ItemType.CurrencyPaid))
            return usersyncData.UserItemDtoInfo.Where(x => x.ItemType == ItemType.CurrencyFree || x.ItemType == ItemType.CurrencyPaid).Sum(d => d.ItemCount);
            
        return usersyncData.UserItemDtoInfo.Where(x => x.ItemType == itemType && (itemId == 0 || x.ItemId == itemId)).Sum(d => d.ItemCount);
    }

    public static UserCharacterDtoInfo? GetUserCharacterDtoInfoByGuid(this UserSyncData userSyncData, string userCharacterGuid)
    {
        return userSyncData.UserCharacterDtoInfos?.FirstOrDefault(d => d.Guid == userCharacterGuid);
    }
}
