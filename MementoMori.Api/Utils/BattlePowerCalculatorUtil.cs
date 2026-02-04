using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Calculators;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Api.Utils;

/// <summary>
/// 战斗力计算工具类
/// 迁移自 MementoMori.Ortega.Common.Utils.BattlePowerCalculatorUtil
/// </summary>
public static class BattlePowerCalculatorUtil
{
    /// <summary>
    /// 根据角色ID获取基础战斗力
    /// </summary>
    public static long GetCharacterBattlePower(long characterId)
    {
        var characterMb = Masters.CharacterTable.GetById(characterId);
        return BattlePowerCalculator.CalculateBattlePower(characterMb.InitialBattleParameter, characterMb.BaseParameterCoefficient);
    }

    /// <summary>
    /// 根据角色ID列表获取总战斗力
    /// </summary>
    public static long GetCharacterBattlePower(List<long> characterIds)
    {
        long result = 0;
        foreach (var characterId in characterIds)
        {
            var characterMb = Masters.CharacterTable.GetById(characterId);
            result += BattlePowerCalculator.CalculateBattlePower(characterMb.InitialBattleParameter, characterMb.BaseParameterCoefficient);
        }

        return result;
    }

    /// <summary>
    /// 根据用户角色GUID获取战斗力
    /// </summary>
    public static long GetUserCharacterBattlePower(NetworkManager nm, string userCharacterGuid, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var info = nm.UserSyncData.GetUserCharacterInfoByUserCharacterGuid(userCharacterGuid);
        if (info == null) return 0;
        return GetUserCharacterBattlePower(nm, info, lockEquipmentDeckType);
    }

    /// <summary>
    /// 根据用户角色DTO信息获取战斗力
    /// </summary>
    public static long GetUserCharacterBattlePower(NetworkManager nm, UserCharacterDtoInfo userCharacterDtoInfo, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var info = nm.UserSyncData.GetUserCharacterInfoByUserCharacterDtoInfo(userCharacterDtoInfo);
        return GetUserCharacterBattlePower(nm, info, lockEquipmentDeckType);
    }

    /// <summary>
    /// 根据用户角色信息获取战斗力
    /// </summary>
    public static long GetUserCharacterBattlePower(NetworkManager nm, UserCharacterInfo userCharacterInfo, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var (baseParameter, battleParameter) = CalcCharacterBattleParameter(nm, userCharacterInfo, lockEquipmentDeckType);
        var battlePower = BattlePowerCalculator.CalculateBattlePower(battleParameter, baseParameter);
        return battlePower;
    }

    /// <summary>
    /// 计算角色战斗参数
    /// </summary>
    public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(NetworkManager nm, string userCharacterGuid,
        LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var syncData = nm.UserSyncData;
        var userCharacterDtoInfo = syncData.GetUserCharacterInfoByUserCharacterGuid(userCharacterGuid);
        return CalcCharacterBattleParameter(nm, userCharacterDtoInfo, lockEquipmentDeckType);
    }

    /// <summary>
    /// 计算角色战斗参数
    /// </summary>
    public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(NetworkManager nm, UserCharacterInfo userCharacterInfo,
        LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var syncData = nm.UserSyncData;
        var rank = syncData.UserStatusDtoInfo.Rank;
        var userEquipmentDtoInfos = syncData.GetUserEquipmentDtoInfosByCharacterGuid(userCharacterInfo.Guid, lockEquipmentDeckType);
        var characterCollectionDtoInfos = syncData.UserCharacterCollectionDtoInfos();
        var characterCollectionParameterInfo = characterCollectionDtoInfos.CalcCharacterCollectionParameterInfo();
        var parameter = userCharacterInfo.CalcCharacterBattleParameter(userEquipmentDtoInfos, characterCollectionParameterInfo, rank);
        return parameter;
    }

    /// <summary>
    /// 获取用户卡组战斗力
    /// </summary>
    public static long GetUserDeckBattlePower(NetworkManager nm, DeckUseContentType deckUseContentType)
    {
        var userDeckDtoInfo = nm.UserSyncData.UserDeckDtoInfos.FirstOrDefault(d => d.DeckUseContentType == deckUseContentType);
        if (userDeckDtoInfo == null) return 0;

        long totalPower = 0;
        foreach (var guid in userDeckDtoInfo.GetUserCharacterGuids())
        {
            totalPower += GetUserCharacterBattlePower(nm, guid);
        }
        return totalPower;
    }

    /// <summary>
    /// 获取角色列表的总战斗力
    /// </summary>
    public static long GetUserCharactersBattlePower(NetworkManager nm, List<string> userCharacterGuids, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        long totalPower = 0;
        foreach (var guid in userCharacterGuids)
        {
            totalPower += GetUserCharacterBattlePower(nm, guid, lockEquipmentDeckType);
        }
        return totalPower;
    }
}
