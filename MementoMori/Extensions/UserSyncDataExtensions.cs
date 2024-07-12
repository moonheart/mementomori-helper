using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Custom;

public static class UserSyncDataExtensions
{
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

    public static UserCharacterDtoInfo GetUserCharacterDtoInfoByGuid(this UserSyncData userSyncData, string userCharacterGuid)
    {
        return userSyncData.UserCharacterDtoInfos.FirstOrDefault(d => d.Guid == userCharacterGuid);
    }


    public static bool IsLevelLinkMember(this UserSyncData userSyncData, string userCharacterGuid)
    {
        return userSyncData.UserLevelLinkMemberDtoInfos.Exists(d => d.UserCharacterGuid == userCharacterGuid);
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

    public static List<UserCharacterCollectionDtoInfo> UserCharacterCollectionDtoInfos(this UserSyncData syncData)
    {
        return syncData.UserCharacterCollectionDtoInfos;
    }

    public static Dictionary<EquipmentSlotType, UserEquipmentDtoInfo> GetUserEquipmentDtoInfoSlotTypeDictionaryByCharacterGuid(this UserSyncData userSyncData, string characterGuid)
    {
        var equipmentSlotTypes = EnumUtil.GetValueList<EquipmentSlotType>();
        var userEquipmentDtoInfos = new Dictionary<EquipmentSlotType, UserEquipmentDtoInfo>();
        foreach (var equipmentSlotType in equipmentSlotTypes)
        {
            userEquipmentDtoInfos[equipmentSlotType] = null;
        }

        if (string.IsNullOrEmpty(characterGuid)) return userEquipmentDtoInfos;

        foreach (var userEquipmentDtoInfo in userSyncData.UserEquipmentDtoInfos.Where(d => d.CharacterGuid == characterGuid))
        {
            var equipmentMb = EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId);
            userEquipmentDtoInfos[equipmentMb.SlotType] = userEquipmentDtoInfo;
        }

        return userEquipmentDtoInfos;
    }

    public static int GetChangedSetEquipmentCount(this UserSyncData userSyncData, string userCharacterGuid, long equipmentSetId, EquipmentSlotType slotType)
    {
        if (userCharacterGuid.IsNullOrEmpty() || equipmentSetId <= 0) return 0;

        var dict = userSyncData.GetUserEquipmentDtoInfoSlotTypeDictionaryByCharacterGuid(userCharacterGuid);
        var count = 0;
        foreach (var (equipmentSlotType, userEquipmentDtoInfo) in dict)
        {
            if (equipmentSlotType != slotType)
            {
                if (EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId).EquipmentSetId != equipmentSetId)
                    continue;
            }

            count++;
        }

        return count;
    }

    public static long GetUserItemCount(this UserSyncData usersyncData, ItemType itemType, long itemId = 0, bool isAnyCurrency = false)
    {
        if (isAnyCurrency && (itemType == ItemType.CurrencyFree || itemType == ItemType.CurrencyPaid))
            return usersyncData?.UserItemDtoInfo?.ToList().Where(x => x.ItemType == ItemType.CurrencyFree || x.ItemType == ItemType.CurrencyPaid).Sum(d => d.ItemCount) ?? 0;
        return usersyncData?.UserItemDtoInfo?.ToList().Where(x => x.ItemType == itemType && (itemId == 0 || x.ItemId == itemId)).Sum(d => d.ItemCount) ?? 0;
    }

    public static List<(UserItemDtoInfo, SphereMB)> GetSpheres(this UserSyncData userSyncData)
    {
        return userSyncData.UserItemDtoInfo.ToList().FindAll(x => x.ItemType == ItemType.Sphere && x.ItemCount > 0).Select(x =>
        {
            var sphereMb = SphereTable.GetById(x.ItemId);
            return (x, sphereMb);
        }).ToList();
    }

    public static List<UserCharacterDtoInfo> GetDeckUserCharacterDtoInfos(this UserSyncData userSyncData, DeckUseContentType deckUseContentType)
    {
        var userDeckDtoInfo = userSyncData.UserDeckDtoInfos.Find(d => d.DeckUseContentType == deckUseContentType);
        if (userDeckDtoInfo == null) return new List<UserCharacterDtoInfo>();

        var guids = userDeckDtoInfo.GetUserCharacterGuids();
        return userSyncData.UserCharacterDtoInfos.Where(d => guids.Contains(d.Guid)).ToList();
    }
}