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

    public static long GetLevelLinkLevel(this UserSyncData userSyncData, long characterId)
    {
        var byId = CharacterTable.GetById(characterId);
        if (byId != null)
        {
            var RarityFlags = byId.RarityFlags;
            if (!OrtegaConst.LevelLink.MaxCharacterLevel.TryGetValue(RarityFlags, out var MaxCharacterLevel)) MaxCharacterLevel = userSyncData.UserLevelLinkDtoInfo.PartyLevel;

            return Math.Min(MaxCharacterLevel, userSyncData.UserLevelLinkDtoInfo.PartyLevel);
        }

        return 0;
    }
    public static List<UserCharacterCollectionDtoInfo> UserCharacterCollectionDtoInfos(this UserSyncData syncData)
    {
        return syncData.UserCharacterCollectionDtoInfos;
    }
    public static bool IsLevelLinkMember(this UserSyncData userSyncData, string userCharacterGuid)
    {
        return userSyncData.UserLevelLinkMemberDtoInfos.Exists(d => d.UserCharacterGuid == userCharacterGuid);
    }

    public static List<UserEquipmentDtoInfo> GetUserEquipmentDtoInfosByCharacterGuid(this UserSyncData userSyncData, string characterGuid,
        LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        if (userSyncData.LockedEquipmentCharacterGuidListMap.TryGetValue(lockEquipmentDeckType, out var guids) && !guids.IsNullOrEmpty())
            return userSyncData.GetLockedUserEquipmentDtoInfosByCharacterGuid(characterGuid, lockEquipmentDeckType);

        var userEquipmentDtoInfos = new List<UserEquipmentDtoInfo>();
        if (!string.IsNullOrEmpty(characterGuid)) userEquipmentDtoInfos.AddRange(userSyncData.UserEquipmentDtoInfos.Where(equipmentDtoInfo => equipmentDtoInfo.CharacterGuid == characterGuid));

        return userEquipmentDtoInfos;
    }

    public static List<UserEquipmentDtoInfo> GetLockedUserEquipmentDtoInfosByCharacterGuid(this UserSyncData syncData, string characterGuid,
        LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var userEquipmentDtoInfos = new List<UserEquipmentDtoInfo>();
        if (characterGuid.IsNullOrEmpty() || !syncData.LockedUserEquipmentDtoInfoListMap.TryGetValue(lockEquipmentDeckType, out var userEquipmentDtoInfos1)) return userEquipmentDtoInfos;

        return userEquipmentDtoInfos1.Where(d => d.Guid == characterGuid).ToList();
    }

    public static UserCharacterInfo GetUserCharacterInfoByUserCharacterDtoInfo(this UserSyncData userSyncData, UserCharacterDtoInfo userCharacterDtoInfo)
    {
        var level = userSyncData.GetLevelLinkLevel(userCharacterDtoInfo.CharacterId);
        var subLevel = 0;
        if (userSyncData.IsLevelLinkMember(userCharacterDtoInfo.Guid))
            subLevel = userSyncData.UserLevelLinkDtoInfo.PartySubLevel;
        else
            level = userCharacterDtoInfo.Level;

        var userCharacterInfo = new UserCharacterInfo
        {
            Guid = userCharacterDtoInfo.Guid,
            CharacterId = userCharacterDtoInfo.CharacterId,
            Exp = userCharacterDtoInfo.Exp,
            IsLocked = userCharacterDtoInfo.IsLocked,
            Level = level,
            SubLevel = subLevel,
            PlayerId = userCharacterDtoInfo.PlayerId,
            RarityFlags = userCharacterDtoInfo.RarityFlags
        };
        return userCharacterInfo;
    }

    public static UserCharacterInfo GetUserCharacterInfoByUserCharacterGuid(this UserSyncData userSyncData, string userCharacterGuid)
    {
        var userCharacterDtoInfoByGuid = userSyncData.GetUserCharacterDtoInfoByGuid(userCharacterGuid);
        if (userCharacterDtoInfoByGuid == null) return null;

        return userSyncData.GetUserCharacterInfoByUserCharacterDtoInfo(userCharacterDtoInfoByGuid);
    }
}