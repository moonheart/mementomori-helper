using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Data;
using Ortega.Share;

namespace MementoMori.Ortega.Custom;

public static class UserSyncDataExtensions
{
    public static UserCharacterInfo GetUserCharacterInfoByUserCharacterDtoInfo(this UserSyncData userSyncData, UserCharacterDtoInfo userCharacterDtoInfo)
    {
        long levelLinkLevel = userSyncData.GetLevelLinkLevel(userCharacterDtoInfo.CharacterId);
        int subLevel = 0;
        if (userSyncData.IsLevelLinkMember(userCharacterDtoInfo.Guid))
        {
            subLevel = userSyncData.UserLevelLinkDtoInfo.PartySubLevel;
        }
        else
        {
            levelLinkLevel = userCharacterDtoInfo.Level;
        }

        UserCharacterInfo userCharacterInfo = new UserCharacterInfo
        {
            Guid = userCharacterDtoInfo.Guid,
            CharacterId = userCharacterDtoInfo.CharacterId,
            Exp = userCharacterDtoInfo.Exp,
            IsLocked = userCharacterDtoInfo.IsLocked,
            Level = levelLinkLevel,
            SubLevel = subLevel,
            PlayerId = userCharacterDtoInfo.PlayerId,
            RarityFlags = userCharacterDtoInfo.RarityFlags
        };
        return userCharacterInfo;
    }
    
    public static bool IsLevelLinkMember(this UserSyncData userSyncData, string userCharacterGuid)
    {
        return userSyncData.UserLevelLinkMemberDtoInfos.Exists(d => d.UserCharacterGuid == userCharacterGuid);
    }
    public static long GetLevelLinkLevel(this UserSyncData userSyncData, long characterId)
    {
        CharacterMB byId = Masters.CharacterTable.GetById(characterId);
        if (byId != null)
        {
            CharacterRarityFlags RarityFlags = byId.RarityFlags;
            if (!OrtegaConst.LevelLink.MaxCharacterLevel.TryGetValue(RarityFlags, out var MaxCharacterLevel))
            {
                MaxCharacterLevel = userSyncData.UserLevelLinkDtoInfo.PartyLevel;
            }

            return Math.Min(MaxCharacterLevel, userSyncData.UserLevelLinkDtoInfo.PartyLevel);
        }

        return 0;
    }

    public static List<UserEquipmentDtoInfo> GetUserEquipmentDtoInfosByCharacterGuid(this UserSyncData userSyncData, string characterGuid, LockEquipmentDeckType lockEquipmentDeckType)
    {
        if(userSyncData.LockedEquipmentCharacterGuidListMap.TryGetValue(lockEquipmentDeckType, out var guids) && !guids.IsNullOrEmpty())
        {
            return userSyncData.GetLockedUserEquipmentDtoInfosByCharacterGuid(characterGuid, lockEquipmentDeckType);
        }

        var userEquipmentDtoInfos = new List<UserEquipmentDtoInfo>();
        if (!string.IsNullOrEmpty(characterGuid))
        {
            userEquipmentDtoInfos.AddRange(userSyncData.UserEquipmentDtoInfos.Where(equipmentDtoInfo => equipmentDtoInfo.CharacterGuid == characterGuid));
        }

        return userEquipmentDtoInfos;
    }

    public static List<UserEquipmentDtoInfo> GetLockedUserEquipmentDtoInfosByCharacterGuid(this UserSyncData syncData, string characterGuid, LockEquipmentDeckType lockEquipmentDeckType)
    {
        var userEquipmentDtoInfos = new List<UserEquipmentDtoInfo>();
        if (characterGuid.IsNullOrEmpty() || !syncData.LockedUserEquipmentDtoInfoListMap.TryGetValue(lockEquipmentDeckType, out var userEquipmentDtoInfos1))
        {
            return userEquipmentDtoInfos;
        }

        return userEquipmentDtoInfos1.Where(d => d.Guid == characterGuid).ToList();
    }

    public static List<UserCharacterCollectionDtoInfo> UserCharacterCollectionDtoInfos(this UserSyncData syncData)
    {
        return syncData.UserCharacterCollectionDtoInfos;
    }

}