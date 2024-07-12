using MementoMori.Common;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Calculators;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Ortega.Common.Utils;

public static class BattlePowerCalculatorUtil
{
    public static long GetCharacterBattlePower(long characterId)
    {
        var characterMb = CharacterTable.GetById(characterId);
        return BattlePowerCalculator.CalculateBattlePower(characterMb.InitialBattleParameter, characterMb.BaseParameterCoefficient);
    }

    public static long GetCharacterBattlePower(List<long> characterIds)
    {
        long result = 0;
        foreach (var characterId in characterIds)
        {
            var characterMb = CharacterTable.GetById(characterId);
            result += BattlePowerCalculator.CalculateBattlePower(characterMb.InitialBattleParameter, characterMb.BaseParameterCoefficient);
        }

        return result;
    }

    public static long GetUserCharacterBattlePower(long userId, string userCharacterGuid, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var info = Services.Get<AccountManager>().Get(userId).Funcs.UserSyncData.GetUserCharacterInfoByUserCharacterGuid(userCharacterGuid);
        if (info == null) return 0;
        return GetUserCharacterBattlePower(userId, info, lockEquipmentDeckType);
    }

    public static long GetUserCharacterBattlePower(long userId, UserCharacterDtoInfo userCharacterDtoInfo, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var info = Services.Get<AccountManager>().Get(userId).Funcs.UserSyncData.GetUserCharacterInfoByUserCharacterDtoInfo(userCharacterDtoInfo);
        return GetUserCharacterBattlePower(userId, info, lockEquipmentDeckType);
    }

    public static long GetUserCharacterBattlePower(long userId, UserCharacterInfo userCharacterInfo, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var (baseParameter, battleParameter) = CalcCharacterBattleParameter(userId, userCharacterInfo, lockEquipmentDeckType);
        var battlePower = BattlePowerCalculator.CalculateBattlePower(battleParameter, baseParameter);
        // var characterMb = Masters.CharacterTable.GetById(userCharacterInfo.CharacterId);
        // var name = Masters.TextResourceTable.Get(characterMb.NameKey);
        // Directory.CreateDirectory("./battlePowerDebug");
        // File.WriteAllText($"./battlePowerDebug/{name}_{userCharacterInfo.RarityFlags}_{userCharacterInfo.Level}_{userCharacterInfo.Guid}.json",JsonConvert.SerializeObject(new
        // {
        // 	BattlePower = battlePower,
        // 	BaseParameter = baseParameter,
        // 	BattleParameter = battleParameter,
        // }, Formatting.Indented));
        return battlePower;
    }

    public static long GetUserCharacterBattlePower(List<string> userCharacterGuids, LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        // int num3;
        // do
        // {
        // 	int num = 0;
        // 	int num2 = 0;
        // 	List<UserCharacterInfo> list = new List();
        // 	List<CharacterMB> list2 = new List();
        // 	bool flag;
        // 	if (flag)
        // 	{
        // 		UserDataManager instance = SingletonMonoBehaviour.Instance;
        // 		UserCharacterInfo userCharacterInfo;
        // 		while (userCharacterInfo == 0)
        // 		{
        // 		}
        // 		list.Add(userCharacterInfo);
        // 		long CharacterId = userCharacterInfo.CharacterId;
        // 		CharacterMB characterMB;
        // 		list2.Add(characterMB);
        // 	}
        // 	if (num2 != 0)
        // 	{
        // 		goto IL_D0;
        // 	}
        // 	num3 = 0;
        // 	int size = list._size;
        // 	Dictionary<ElementType, List<BattleParameterChangeInfo>> dictionary = BattleParameterExtension.CalcElementParameterBonus(list2, size);
        // 	bool flag2;
        // 	if (flag2)
        // 	{
        // 		UserDataManager instance2 = SingletonMonoBehaviour.Instance;
        // 		int num4 = 0;
        // 		long rank = instance2.Rank;
        // 		List<UserEquipmentDtoInfo> userEquipmentDtoInfosByCharacterGuid = SingletonMonoBehaviour.Instance.GetUserEquipmentDtoInfosByCharacterGuid(num4);
        // 		CharacterCollectionParameterInfo characterCollectionParameterInfo = BattleParameterExtension.CalcCharacterCollectionParameterInfo(SingletonMonoBehaviour.Instance.UserCharacterCollectionDtoInfos);
        // 		ElementType ElementType = Masters.CharacterTable.GetById((long)num).ElementType;
        // 		ElementType orDefault = dictionary.GetOrDefault(ElementType);
        // 		long num5;
        // 		num3 = (int)((long)num3 + num5);
        // 	}
        // }
        // while (num3 != 0);
        // throw new NullReferenceException();
        // IL_D0:
        // throw new NullReferenceException();
        throw new NotImplementedException();
    }

    public static long GetUserCharacterNextRarityBattlePower(UserCharacterDtoInfo userCharacterDtoInfo)
    {
        throw new NotImplementedException();
    }

    public static long GetUserCharacterNextLevelBattlePower(UserCharacterDtoInfo userCharacterDtoInfo)
    {
        throw new NotImplementedException();
    }

    public static long GetUserCharacterPrevLevelBattlePower(UserCharacterDtoInfo userCharacterDtoInfo)
    {
        throw new NotImplementedException();
    }

    public static long GetUserDeckBattlePower(DeckUseContentType deckUseContentType)
    {
        // UserDataManager instance = SingletonMonoBehaviour.Instance;
        // UserDeckDtoInfo userDeckDtoInfo;
        // return BattlePowerCalculatorUtil.GetUserCharacterBattlePower(userDeckDtoInfo.GetUserCharacterGuids());
        throw new NotImplementedException();
    }

    public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(long userId, string userCharacterGuid,
        LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var syncData = Services.Get<AccountManager>().Get(userId).Funcs.UserSyncData;
        var userCharacterDtoInfo = syncData.GetUserCharacterInfoByUserCharacterGuid(userCharacterGuid);
        return CalcCharacterBattleParameter(userId, userCharacterDtoInfo, lockEquipmentDeckType);
    }

    public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(long userId, UserCharacterInfo userCharacterInfo,
        LockEquipmentDeckType lockEquipmentDeckType = LockEquipmentDeckType.None)
    {
        var syncData = Services.Get<AccountManager>().Get(userId).Funcs.UserSyncData;
        var rank = syncData.UserStatusDtoInfo.Rank;
        var userEquipmentDtoInfos = syncData.GetUserEquipmentDtoInfosByCharacterGuid(userCharacterInfo.Guid, lockEquipmentDeckType);
        var characterCollectionDtoInfos = syncData.UserCharacterCollectionDtoInfos();
        var characterCollectionParameterInfo = characterCollectionDtoInfos.CalcCharacterCollectionParameterInfo();
        var parameter = userCharacterInfo.CalcCharacterBattleParameter(userEquipmentDtoInfos, characterCollectionParameterInfo, rank);
        return parameter;
    }

    public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameterForPictureBook(long characterId, CharacterRarityFlags rarityFlags, long level)
    {
        // CharacterTable CharacterTable = Masters.CharacterTable;
        // CharacterMB characterMB;
        // BaseParameterType mainParameterType = CharacterUtil.GetMainParameterType(characterMB.JobFlags);
        // BattleParameter InitialBattleParameter = characterMB.InitialBattleParameter;
        // int num = 0;
        // BattleParameter battleParameter = InitialBattleParameter.DeepCopy();
        // battleParameter.HP = (long)num;
        // BaseParameter baseParameter;
        // battleParameter.AttackPower = baseParameter;
        // long PhysicalDamageRelax = battleParameter.PhysicalDamageRelax;
        // battleParameter.PhysicalDamageRelax = PhysicalDamageRelax;
        // long MagicDamageRelax = battleParameter.MagicDamageRelax;
        // battleParameter.MagicDamageRelax = MagicDamageRelax;
        // int num2 = 0;
        // characterId.m_value = (long)num2;
        // throw new NullReferenceException();
        throw new NotImplementedException();
    }
}