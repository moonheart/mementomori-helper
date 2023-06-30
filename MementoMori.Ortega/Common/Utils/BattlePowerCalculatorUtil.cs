using System.Runtime.InteropServices;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.Ortega.Common.Utils
{
	public static class BattlePowerCalculatorUtil
	{
		public static long GetCharacterBattlePower(long characterId)
		{
			// CharacterMB byId = Masters.CharacterTable.GetById(characterId);
			// if (byId != 0)
			// {
			// 	BaseParameter BaseParameterCoefficient = byId.BaseParameterCoefficient;
			// 	return BattlePowerCalculator.CalculateBattlePower(byId.InitialBattleParameter, BaseParameterCoefficient);
			// }
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static long GetCharacterBattlePower(List<long> characterIds)
		{
			// int num;
			// do
			// {
			// 	num = 0;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		CharacterTable CharacterTable = Masters.CharacterTable;
			// 		CharacterMB characterMB;
			// 		while (characterMB == 0)
			// 		{
			// 		}
			// 		BaseParameter BaseParameterCoefficient = characterMB.BaseParameterCoefficient;
			// 		long num2 = BattlePowerCalculator.CalculateBattlePower(characterMB.InitialBattleParameter, BaseParameterCoefficient);
			// 		num = (int)((long)num + num2);
			// 	}
			// }
			// while (num != 0);
			// throw new NullReferenceException();
			throw new NotImplementedException();

		}

		public static long GetUserCharacterBattlePower(string userCharacterGuid, [Optional] Dictionary<ElementType, List<BattleParameterChangeInfo>> elementParameterBonusDict)
		{
			throw new NotImplementedException();
		}

		public static long GetUserCharacterBattlePower(UserCharacterDtoInfo userCharacterDtoInfo, [Optional] Dictionary<ElementType, List<BattleParameterChangeInfo>> elementParameterBonusDict)
		{
			throw new NotImplementedException();
		}

		public static long GetUserCharacterBattlePower(UserCharacterInfo userCharacterInfo, [Optional] Dictionary<ElementType, List<BattleParameterChangeInfo>> elementParameterBonusDict)
		{
			throw new NotImplementedException();
		}

		public static long GetUserCharacterBattlePower(List<string> userCharacterGuids)
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

		public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(string userCharacterGuid, [Optional] Dictionary<ElementType, List<BattleParameterChangeInfo>> elementParameterBonusDict)
		{
			// UserDataManager instance = SingletonMonoBehaviour.Instance;
			// throw new NullReferenceException();
			throw new NotImplementedException();

		}

		public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(UserCharacterInfo userCharacterInfo, [Optional] Dictionary<ElementType, List<BattleParameterChangeInfo>> elementParameterBonusDict)
		{
			// long rank = SingletonMonoBehaviour.Instance.Rank;
			// UserDataManager instance = SingletonMonoBehaviour.Instance;
			// CharacterCollectionParameterInfo characterCollectionParameterInfo = BattleParameterExtension.CalcCharacterCollectionParameterInfo(SingletonMonoBehaviour.Instance.UserCharacterCollectionDtoInfos);
			// throw new NullReferenceException();
			throw new NotImplementedException();

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
}
