using MementoMori.Ortega.Share.Data.BountyQuest;
using MementoMori.Ortega.Share.Data.DtoInfo;

namespace MementoMori.Utils;

public static class BountyQuestUtil
{
    public static bool IsQuestEnable(UserBountyQuestDtoInfo questDtoInfo)
    {
        // questDtoInfo.StartMembers
        // List<BountyQuestMemberInfo> <StartMembers>k__BackingField = questDtoInfo.StartMembers;
        // long <BountyQuestLimitStartTime>k__BackingField = questDtoInfo.BountyQuestLimitStartTime;
        // long localTimestamp = TimeManager.Instance.GetLocalTimestamp();
        // if (<BountyQuestLimitStartTime>k__BackingField > localTimestamp)
        // {
        // 	return true;
        // }
        // int num = 0;
        throw new NullReferenceException();
    }

    public static bool IsNotStartedQuest(UserBountyQuestDtoInfo questDtoInfo)
    {
        // List<BountyQuestMemberInfo> <StartMembers>k__BackingField = questDtoInfo.StartMembers;
        // long <BountyQuestLimitStartTime>k__BackingField = questDtoInfo.BountyQuestLimitStartTime;
        // long localTimestamp = TimeManager.Instance.GetLocalTimestamp();
        // if (<BountyQuestLimitStartTime>k__BackingField > localTimestamp)
        // {
        // 	return true;
        // }
        // int num = 0;
        throw new NullReferenceException();
    }

    public static bool IsRewardGet(UserBountyQuestDtoInfo questDtoInfo)
    {
        // List<BountyQuestMemberInfo> <StartMembers>k__BackingField = questDtoInfo.StartMembers;
        // if (!questDtoInfo.IsReward)
        // {
        // 	long localTimestamp = TimeManager.Instance.GetLocalTimestamp();
        // 	if (questDtoInfo.RewardEndTime > localTimestamp)
        // 	{
        // 		return true;
        // 	}
        // }
        // int num = 0;
        throw new NullReferenceException();
    }

    public static List<UserCharacterDtoInfo> GetReadyCharacters(List<UserBountyQuestDtoInfo> questDtoInfos, BountyQuestType bountyQuestType)
    {
        // //IL_0015: Expected I4, but got O
        // //IL_004d: Expected O, but got I4
        // bool flag = default(bool);
        // bool flag2 = default(bool);
        // bool flag3 = default(bool);
        // bool flag4 = default(bool);
        // bool flag5 = default(bool);
        // bool flag6 = default(bool);
        // while (true)
        // {
        // 	int num = 0;
        // 	int num2 = 0;
        // 	int num3 = 0;
        // 	List<UserCharacterDtoInfo> list = (List<UserCharacterDtoInfo>)(object)new List<T>((int)((UserDataManager)(object)SingletonMonoBehaviour<T>.Instance).UserCharacterDtoInfos);
        // 	int num4 = 0;
        // 	if (flag)
        // 	{
        // 		int num5 = 0;
        // 		while (!flag2)
        // 		{
        // 		}
        // 		if (flag3)
        // 		{
        // 			long playerId = ((UserDataManager)(object)SingletonMonoBehaviour<T>.Instance).PlayerId;
        // 			while (flag4)
        // 			{
        // 			}
        // 			if (flag5)
        // 			{
        // 				while (!flag6)
        // 				{
        // 				}
        // 				bool flag7 = ((List<T>)(object)list).Remove((T)num2);
        // 			}
        // 			if (num4 != 0)
        // 			{
        // 				throw new NullReferenceException();
        // 			}
        // 		}
        // 		if (num4 != 0)
        // 		{
        // 			break;
        // 		}
        // 	}
        // 	if (num4 == 0)
        // 	{
        // 		return list;
        // 	}
        // }
        throw new NullReferenceException();
    }

    public static bool CheckQuestMember(BountyQuestInfo questInfo, List<string> userCharacterGuids, UserBountyQuestMemberDtoInfo memberDtoInfo)
    {
        // bool flag = default(bool);
        // bool flag2 = default(bool);
        // ulong num2 = default(ulong);
        // do
        // {
        // 	Dictionary<ElementType, int> elementCounts = (Dictionary<ElementType, int>)(object)BountyQuestUtil.GetElementCounts((List<>)(object)userCharacterGuids, memberDtoInfo);
        // 	List<BountyQuestConditionInfo> <BountyQuestConditionInfos>k__BackingField = questInfo.BountyQuestConditionInfos;
        // 	if (flag)
        // 	{
        // 		if (!flag)
        // 		{
        // 		}
        // 		while (!flag)
        // 		{
        // 		}
        // 		while (flag2)
        // 		{
        // 		}
        // 		int num = 0;
        // 	}
        // }
        // while (num2 != 0);
        throw new NullReferenceException();
    }

    public static bool CheckConditionInfosRarity(BountyQuestConditionInfo conditionInfo, List<string> userCharacterGuids, UserBountyQuestMemberDtoInfo memberDtoInfo)
    {
        // bool flag = default(bool);
        // UserCharacterDtoInfo userCharacterDtoInfo = default(UserCharacterDtoInfo);
        // int num;
        // do
        // {
        // 	num = 0;
        // 	CharacterRarityFlags <Rarity>k__BackingField = conditionInfo.Rarity;
        // 	if (flag)
        // 	{
        // 		UserDataManager instance = (UserDataManager)(object)SingletonMonoBehaviour<T>.Instance;
        // 		while (userCharacterDtoInfo == null)
        // 		{
        // 		}
        // 		num++;
        // 	}
        // }
        // while (num != 0);
        // num++;
        // return unchecked((nint)"{il2cpp field on {'constant25' (constant value of type Cpp2IL.Core.Analysis.ResultModels.StackPointer)}, offset 0xX}") <= num;
        throw new NullReferenceException();
    }

    public static Dictionary<ElementType, int> GetElementCounts(List<string> userCharacterGuids, UserBountyQuestMemberDtoInfo memberDtoInfo)
    {
        // //IL_0040: Expected O, but got I4
        // bool flag = default(bool);
        // ulong num3 = default(ulong);
        // CharacterMB characterMB = default(CharacterMB);
        // bool flag2 = default(bool);
        // ulong num4 = default(ulong);
        // while (true)
        // {
        // 	int num = 0;
        // 	int num2 = 0;
        // 	Dictionary<ElementType, int> result = (Dictionary<ElementType, int>)(object)new Dictionary<TKey, TValue>(EnumUtil.GetValueList<ElementType>()._size);
        // 	if (flag)
        // 	{
        // 	}
        // 	if (num3 != 0)
        // 	{
        // 		break;
        // 	}
        // 	CharacterTable <CharacterTable>k__BackingField = Masters.CharacterTable;
        // 	ElementType <ElementType>k__BackingField = characterMB.ElementType;
        // 	if (flag2)
        // 	{
        // 		UserCharacterDtoInfo userCharacterDtoInfoByGuid = ((UserDataManager)(object)SingletonMonoBehaviour<T>.Instance).GetUserCharacterDtoInfoByGuid((string)num2);
        // 		CharacterTable <CharacterTable>k__BackingField2 = Masters.CharacterTable;
        // 		long <CharacterId>k__BackingField = userCharacterDtoInfoByGuid.CharacterId;
        // 		ElementType <ElementType>k__BackingField2 = ((CharacterMB)(object)((TableBase<TM>)(object)<CharacterTable>k__BackingField2).GetById(<CharacterId>k__BackingField)).ElementType;
        // 	}
        // 	if (num4 == 0)
        // 	{
        // 		return result;
        // 	}
        // }
        throw new NullReferenceException();
    }
}