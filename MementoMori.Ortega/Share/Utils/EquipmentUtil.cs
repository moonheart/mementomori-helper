using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Item.Model;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.Ortega.Share.Utils
{
	public static class EquipmentUtil
	{
		public static ValueTuple<List<IUserItem>, List<IUserItem>> GetComposeLackSphereResult(IEnumerable<IUserItem> sphereList, long sphereId)
		{
			throw new NotImplementedException();
			// throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
		}

		public static ValueTuple<List<IUserItem>, List<IUserItem>, List<IUserItem>> GetComposeSphereResult(IEnumerable<IUserItem> sphereList, List<long> categoryIdList)
		{
			// Dictionary<long, long> dictionary3;
			// Dictionary<long, long> dictionary4;
			// List<IUserItem> list;
			// ulong num7;
			// do
			// {
			// 	int num = 0;
			// 	SphereTable SphereTable = Masters.SphereTable;
			// 	if (EquipmentUtil.<>c.<>9__1_0 == 0)
			// 	{
			// 		Func<SphereMB, long> func;
			// 		EquipmentUtil.<>c.<>9__1_0 = func;
			// 	}
			// 	Func<IGrouping<long, SphereMB>, long> func2;
			// 	if (EquipmentUtil.<>c.<>9__1_1 == 0)
			// 	{
			// 		EquipmentUtil.<>c.<>9__1_1 = func2;
			// 	}
			// 	Func<IGrouping<long, SphereMB>, List<SphereMB>> func3;
			// 	if (EquipmentUtil.<>c.<>9__1_2 == 0)
			// 	{
			// 		EquipmentUtil.<>c.<>9__1_2 = func3;
			// 	}
			// 	IEnumerable enumerable;
			// 	Dictionary<IGrouping<long, SphereMB>, long> dictionary = Enumerable.ToDictionary<IGrouping<long, SphereMB>, long, List<SphereMB>>(enumerable, func2, func3);
			// 	Func<IUserItem, bool> func4;
			// 	if (EquipmentUtil.<>c.<>9__1_3 == 0)
			// 	{
			// 		EquipmentUtil.<>c.<>9__1_3 = func4;
			// 	}
			// 	IEnumerable<IUserItem> enumerable2 = categoryIdList.Where(func4);
			// 	Func<IUserItem, long> func5;
			// 	if (EquipmentUtil.<>c.<>9__1_4 == 0)
			// 	{
			// 		EquipmentUtil.<>c.<>9__1_4 = func5;
			// 	}
			// 	Func<IUserItem, long> func6;
			// 	if (EquipmentUtil.<>c.<>9__1_5 == 0)
			// 	{
			// 		EquipmentUtil.<>c.<>9__1_5 = func6;
			// 	}
			// 	Dictionary<IUserItem, long> dictionary2 = Enumerable.ToDictionary<IUserItem, long, long>(enumerable2, func5, func6);
			// 	dictionary3 = new Dictionary();
			// 	dictionary4 = new Dictionary();
			// 	list = new List();
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		bool flag2;
			// 		if (flag2)
			// 		{
			// 			EquipmentUtil.<>c__DisplayClass1_0 CS$<>8__locals1;
			// 			CS$<>8__locals1.sphereMB = num;
			// 			SphereMB sphereMB = CS$<>8__locals1.sphereMB;
			// 			long Id = CS$>8__locals1.sphereMB.<Id;
			// 			long num2 = dictionary4.GetOrDefault(Id);
			// 			long num3;
			// 			num2 += num3;
			// 			num2 -= Id;
			// 			EquipmentUtil.<>c__DisplayClass1_1 CS$<>8__locals2;
			// 			CS$<>8__locals2.nextCount = num2;
			// 			List<SphereMB> list2;
			// 			Func<SphereMB, bool> func7;
			// 			SphereMB sphereMB2 = Enumerable.FirstOrDefault<SphereMB>(list2, func7);
			// 			if (sphereMB2 != 0)
			// 			{
			// 				func7 -= num2;
			// 				long Id2 = CS$>8__locals1.sphereMB.<Id;
			// 				long num4 = dictionary4[Id2];
			// 				long num5 = CS$<>8__locals2.nextCount;
			// 				num5 += num5;
			// 				num4 -= num5;
			// 				dictionary4[Id2] = num4;
			// 				bool flag3 = dictionary4.Remove(Id2);
			// 				long Id3 = CS$>8__locals1.sphereMB.<Id;
			// 				dictionary3.Add(Id3, Id2);
			// 				long nextCount = CS$<>8__locals2.nextCount;
			// 				long Id4 = sphereMB2.Id;
			// 				dictionary4[Id4] = nextCount;
			// 				Func<UserItem, UserItem> func8;
			// 				IEnumerableUserItem> enumerable3 = CS$<>8__locals1.sphereMB.<ItemListRequiredToCombine.Select(func8);
			// 				list.AddRange(enumerable3);
			// 			}
			// 		}
			// 		ulong num6;
			// 		if (num6 != (ulong)0L)
			// 		{
			// 			goto IL_258;
			// 		}
			// 	}
			// }
			// while (num7 != (ulong)0L);
			// Func<KeyValuePair<long, long>, IUserItem> func9;
			// if (EquipmentUtil.<>c.<>9__1_6 == 0)
			// {
			// 	EquipmentUtil.<>c.<>9__1_6 = func9;
			// }
			// List<IUserItem> list3 = Enumerable.ToList<IUserItem>(dictionary3.Select(func9));
			// Func<KeyValuePair<long, long>, IUserItem> func10;
			// if (EquipmentUtil.<>c.<>9__1_7 == 0)
			// {
			// 	EquipmentUtil.<>c.<>9__1_7 = func10;
			// }
			// IEnumerable<KeyValuePair<long, long>> enumerable4 = dictionary4.Select(func10);
			// Func<IUserItem, bool> func11;
			// if (EquipmentUtil.<>c.<>9__1_8 == 0)
			// {
			// 	EquipmentUtil.<>c.<>9__1_8 = func11;
			// }
			// List<IUserItem> list4 = Enumerable.ToList<IUserItem>(enumerable4.Where(func11));
			// List<IUserItem> list5 = UserItemProvider.MergeSameItem(list);
			// throw new NullReferenceException();
			// IL_258:
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static long GetSacredMaxLevel(SacredTreasureType sacredTreasureType)
		{
			// if (sacredTreasureType == SacredTreasureType.Legend)
			// {
			// 	EquipmentLegendSacredTreasureTable EquipmentLegendSacredTreasureTable = Masters.EquipmentLegendSacredTreasureTable;
			// 	if (EquipmentUtil.<>c.<>9__2_0 == 0)
			// 	{
			// 		Func<EquipmentLegendSacredTreasureMB, long> func;
			// 		EquipmentUtil.<>c.<>9__2_0 = func;
			// 	}
			// 	throw new NullReferenceException();
			// }
			// if (sacredTreasureType == SacredTreasureType.Matchless)
			// {
			// 	EquipmentMatchlessSacredTreasureTable EquipmentMatchlessSacredTreasureTable = Masters.EquipmentMatchlessSacredTreasureTable;
			// 	if (EquipmentUtil.<>c.<>9__2_1 == 0)
			// 	{
			// 		Func<EquipmentMatchlessSacredTreasureMB, long> func2;
			// 		EquipmentUtil.<>c.<>9__2_1 = func2;
			// 	}
			// 	long num;
			// 	return num;
			// }
			// string text;
			// Exception ex = new Exception(text);
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static BattleParameterChangeInfo GetSacredParameter(EquipmentSlotType slotType, long level, SacredTreasureType treasureType)
		{
			return treasureType switch
			{
				SacredTreasureType.Legend => Masters.EquipmentLegendSacredTreasureTable.GetByLevel(level).GetValue(slotType),
				SacredTreasureType.Matchless => Masters.EquipmentMatchlessSacredTreasureTable.GetByLevel(level).GetValue(slotType),
				_ => new BattleParameterChangeInfo()
			};
		}

		// public static SacredTreasureInfo GetSacredTreasureInfoAfter(this IReadOnlyEquipment equipment, List<IReadOnlyEquipment> equipmentList, List<UserMatchlessSacredTreasureExpItem> matchlessSacredTreasureExpItemList)
		// {
		// 	EquipmentMatchlessSacredTreasureTable EquipmentMatchlessSacredTreasureTable = Masters.EquipmentMatchlessSacredTreasureTable;
		// 	EquipmentUtil.<>c__DisplayClass4_0 CS$<>8__locals1;
		// 	CS$>8__locals1.sacredTreasureMatchlessMB = <EquipmentMatchlessSacredTreasureTable;
		// 	EquipmentLegendSacredTreasureTable EquipmentLegendSacredTreasureTable = Masters.EquipmentLegendSacredTreasureTable;
		// 	EquipmentMatchlessSacredTreasureMB[] sacredTreasureMatchlessMB = CS$<>8__locals1.sacredTreasureMatchlessMB;
		// 	Func<EquipmentMatchlessSacredTreasureMB, long> <>9__4_ = EquipmentUtil.<>c.<>9__4_0;
		// 	if (<>9__4_ == 0)
		// 	{
		// 		Func<EquipmentMatchlessSacredTreasureMB, long> func;
		// 		EquipmentUtil.<>c.<>9__4_0 = func;
		// 	}
		// 	EquipmentMatchlessSacredTreasureMB equipmentMatchlessSacredTreasureMB = Enumerable.FirstOrDefault<EquipmentMatchlessSacredTreasureMB>(Enumerable.OrderByDescending<EquipmentMatchlessSacredTreasureMB, long>(sacredTreasureMatchlessMB, <>9__4_));
		// 	CS$<>8__locals1.maxMatchlessMB = equipmentMatchlessSacredTreasureMB;
		// 	if (EquipmentUtil.<>c.<>9__4_1 == 0)
		// 	{
		// 		Func<EquipmentLegendSacredTreasureMB, long> func2;
		// 		EquipmentUtil.<>c.<>9__4_1 = func2;
		// 	}
		// 	if (CS$<>8__locals1.maxMatchlessMB != (ulong)0L)
		// 	{
		// 		SacredTreasureInfo sacredTreasureInfo = new SacredTreasureInfo();
		// 		List<IReadOnlyEquipment> list = new List();
		// 		sacredTreasureInfo.AbsorbedEquipmentList = list;
		// 		sacredTreasureInfo.LegendSacredTreasureExp = list;
		// 		sacredTreasureInfo.LegendSacredTreasureLv = list;
		// 		sacredTreasureInfo.MatchlessSacredTreasureExp = list;
		// 		sacredTreasureInfo.MatchlessSacredTreasureLv = list;
		// 		List<UserMatchlessSacredTreasureExpItem> list2 = new List();
		// 		sacredTreasureInfo.MatchlessSacredTreasureExpItemList = list2;
		// 		CS$<>8__locals1.info = sacredTreasureInfo;
		// 		Action<UserMatchlessSacredTreasureExpItem> action;
		// 		matchlessSacredTreasureExpItemList.ForEach(action);
		// 		int num = 0;
		// 		bool flag;
		// 		if (flag)
		// 		{
		// 			uint num2;
		// 			if (num < (int)num2)
		// 			{
		// 				num += num;
		// 				num++;
		// 			}
		// 			EquipmentMatchlessSacredTreasureMB equipmentMatchlessSacredTreasureMB2 = CS$<>8__locals1.maxMatchlessMB;
		// 			equipmentMatchlessSacredTreasureMB2 += equipmentMatchlessSacredTreasureMB2;
		// 			uint num3;
		// 			while (num3 != (uint)0)
		// 			{
		// 			}
		// 			SacredTreasureInfo info = CS$<>8__locals1.info;
		// 			long MatchlessSacredTreasureExp = info.MatchlessSacredTreasureExp;
		// 			num3 = (uint)((long)num3 + MatchlessSacredTreasureExp);
		// 			info.MatchlessSacredTreasureExp = (long)num3;
		// 			SacredTreasureInfo info2 = CS$<>8__locals1.info;
		// 			long LegendSacredTreasureExp = info2.LegendSacredTreasureExp;
		// 			num3 = (uint)((long)num3 + LegendSacredTreasureExp);
		// 			info2.LegendSacredTreasureExp = (long)num3;
		// 			SacredTreasureInfo info3 = CS$<>8__locals1.info;
		// 			EquipmentMatchlessSacredTreasureMB[] sacredTreasureMatchlessMB2 = CS$<>8__locals1.sacredTreasureMatchlessMB;
		// 			Func<EquipmentMatchlessSacredTreasureMB, bool> func3;
		// 			if (CS$<>8__locals1.<>9__5 == 0)
		// 			{
		// 				CS$<>8__locals1.<>9__5 = func3;
		// 			}
		// 			IEnumerable<EquipmentMatchlessSacredTreasureMB> enumerable = sacredTreasureMatchlessMB2.Where(func3);
		// 			Func<EquipmentMatchlessSacredTreasureMB, long> func4;
		// 			if (EquipmentUtil.<>c.<>9__4_6 == 0)
		// 			{
		// 				EquipmentUtil.<>c.<>9__4_6 = func4;
		// 			}
		// 			long num4 = Enumerable.Max<EquipmentMatchlessSacredTreasureMB>(enumerable, func4);
		// 			info3.MatchlessSacredTreasureLv = num4;
		// 			SacredTreasureInfo info4 = CS$<>8__locals1.info;
		// 			if (CS$<>8__locals1.<>9__7 == 0)
		// 			{
		// 				Func<EquipmentLegendSacredTreasureMB, bool> func5;
		// 				CS$<>8__locals1.<>9__7 = func5;
		// 			}
		// 			Func<EquipmentLegendSacredTreasureMB, long> func6;
		// 			if (EquipmentUtil.<>c.<>9__4_8 == 0)
		// 			{
		// 				EquipmentUtil.<>c.<>9__4_8 = func6;
		// 			}
		// 			IEnumerable<EquipmentLegendSacredTreasureMB> enumerable2;
		// 			long num5 = Enumerable.Max<EquipmentLegendSacredTreasureMB>(enumerable2, func6);
		// 			info4.LegendSacredTreasureLv = num5;
		// 			ListIReadOnlyEquipment> <AbsorbedEquipmentList = CS$>8__locals1.info.<AbsorbedEquipmentList;
		// 		}
		// 		if (num == 0)
		// 		{
		// 			return CS$<>8__locals1.info;
		// 		}
		// 	}
		// 	Exception ex = new Exception("Not found maxMatchlessMB.");
		// 	throw new NullReferenceException();
		// }

		public static SacredTreasureType GetSacredTreasureType(this IReadOnlyEquipment equipment)
		{
			return equipment switch
			{
				{LegendSacredTreasureLv: 0, MatchlessSacredTreasureLv: 0} => SacredTreasureType.None,
				{LegendSacredTreasureLv: > 0, MatchlessSacredTreasureLv: 0} => SacredTreasureType.Legend,
				{LegendSacredTreasureLv: 0, MatchlessSacredTreasureLv: > 0} => SacredTreasureType.Matchless,
				{LegendSacredTreasureLv: > 0, MatchlessSacredTreasureLv: > 0} => SacredTreasureType.DualStatus,
				_ => SacredTreasureType.None
			};
		}

		public static List<IUserItem> GetItemsAfterDisassemble(UserEquipment userEquipment)
		{
			// List<IUserItem> list = new List();
			// EquipmentTable EquipmentTable = Masters.EquipmentTable;
			// long equipmentId = userEquipment.EquipmentId;
			// EquipmentMB byId = EquipmentTable.GetById(equipmentId);
			// EquipmentCompositeTable EquipmentCompositeTable = Masters.EquipmentCompositeTable;
			// long CompositeId = byId.CompositeId;
			// EquipmentCompositeMB byId2 = EquipmentCompositeTable.GetById(CompositeId);
			// if (byId2 != 0)
			// {
			// 	long RequiredFragmentCount = byId2.RequiredFragmentCount;
			// 	long ItemCount = userEquipment.ItemCount;
			// 	long num;
			// 	UserEquipmentFragment userEquipmentFragment = new UserEquipmentFragment(CompositeId, num);
			// 	num = ItemCount * RequiredFragmentCount;
			// 	list.Add(userEquipmentFragment);
			// }
			// int num2 = 0;
			// if ((long)num2  userEquipment.<ItemCount)
			// {
			// 	List<IUserItem> sumOfEvolutionItem = EquipmentUtil.GetSumOfEvolutionItem(userEquipment);
			// 	list.AddRange(sumOfEvolutionItem);
			// 	num2++;
			// }
			// return list;
			throw new NotImplementedException();
		}

		public static List<IUserItem> GetSumOfEvolutionItem(this IReadOnlyEquipment equipment)
		{
			// EquipmentTable EquipmentTable = Masters.EquipmentTable;
			// EquipmentEvolutionTable EquipmentEvolutionTable = Masters.EquipmentEvolutionTable;
			// EquipmentMB equipmentMB;
			// long EquipmentEvolutionId = equipmentMB.EquipmentEvolutionId;
			// EquipmentEvolutionMB byId = EquipmentEvolutionTable.GetById(EquipmentEvolutionId);
			// HashSet<long> hashSet = new HashSet();
			// List<IUserItem> list = new List();
			// IReadOnlyListEquipmentEvolutionInfo> <EquipmentEvolutionInfoList = byId.EquipmentEvolutionInfoList;
			// int num = 0;
			// if (list != 0)
			// {
			// 	uint num2;
			// 	if (num < (int)num2)
			// 	{
			// 		num += num;
			// 		if (num == (int)num2)
			// 		{
			// 			goto IL_73;
			// 		}
			// 		num++;
			// 	}
			// 	bool flag = hashSet.Add(num);
			// 	bool flag2 = hashSet.Add(num);
			// 	list.AddRange(num);
			// 	IL_73:
			// 	flag2 += flag2;
			// 	num += 312;
			// }
			// if ("{il2cpp array field local14->}" != (ulong)0L)
			// {
			// }
			// if (num == 0)
			// {
			// 	long EquipmentLv = equipmentMB.EquipmentLv;
			// 	if (hashSet.Contains(EquipmentLv))
			// 	{
			// 		return UserItemProvider.MergeSameItem(list);
			// 	}
			// }
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static List<IUserItem> GetSumOfReinforcementItem(List<IReadOnlyEquipment> equipmentList)
		{
			// List<IUserItem> list = new List();
			// List<IUserItem> itemList = list;
			// Action<IReadOnlyEquipment> action;
			// equipmentList.ForEach(action);
			// return UserItemProvider.MergeSameItem(itemList);
			throw new NotImplementedException();
		}

		public static bool IsPossibleEquipmentTraining(UserEquipmentDtoInfo userEquipmentDtoInfo)
		{
			// int num = 0;
			// return (userEquipmentDtoInfo.AdditionalParameterMuscle != (long)num) + true + true + true;
			throw new NotImplementedException();
		}

		public static bool IsMaxLevelSacredTreasure(UserEquipmentDtoInfo userEquipmentDtoInfo)
		{
			long sacredMaxLevel = EquipmentUtil.GetSacredMaxLevel(SacredTreasureType.Matchless);
			long sacredMaxLevel2 = EquipmentUtil.GetSacredMaxLevel(SacredTreasureType.Legend);
			return true;
		}

		public static BattleParameterChangeInfo GetEquipmentReinforcementBaseParameter(long equipmentId, long reinforcementLv)
		{
			EquipmentMB equipmentMb = Masters.EquipmentTable.GetById(equipmentId);
			if (equipmentMb != null)
			{
				var equipmentReinforcementParameterMb = Masters.EquipmentReinforcementParameterTable.GetByLevel(reinforcementLv);
				
				BattleParameterChangeInfo battleParameterChangeInfo = new BattleParameterChangeInfo
				{
					BattleParameterType = equipmentMb.BattleParameterChangeInfo.BattleParameterType,
					ChangeParameterType = equipmentMb.BattleParameterChangeInfo.ChangeParameterType,
					Value = equipmentMb.BattleParameterChangeInfo.Value * equipmentReinforcementParameterMb?.ReinforcementCoefficient ?? 0
				};
				return battleParameterChangeInfo;
			}
			return new BattleParameterChangeInfo();
		}

		private static void AddRarityEvolutionItems(long equipmentId, List<IUserItem> itemList)
		{
			// ulong num7;
			// do
			// {
			// 	int num = 0;
			// 	EquipmentMB byId = Masters.EquipmentTable.GetById(equipmentId);
			// 	if (byId == 0 || byId.Category == EquipmentCategory.Normal)
			// 	{
			// 		break;
			// 	}
			// 	EquipmentTable EquipmentTable = Masters.EquipmentTable;
			// 	EquipmentRarityFlags RarityFlags = byId.RarityFlags;
			// 	long EquipmentEvolutionId = byId.EquipmentEvolutionId;
			// 	List<EquipmentMB> list = new List();
			// 	int num2 = 0;
			// 	num2++;
			// 	if (list.IsNullOrEmpty<EquipmentMB>())
			// 	{
			// 		break;
			// 	}
			// 	int num3 = 0;
			// 	EquipmentMB equipmentMB = list[num3];
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		long EquipmentLv = equipmentMB.EquipmentLv;
			// 	}
			// 	ulong num4;
			// 	if (num4 != (ulong)0L)
			// 	{
			// 		goto IL_104;
			// 	}
			// 	if (num == 0)
			// 	{
			// 		break;
			// 	}
			// 	EquipmentTable EquipmentTable2 = Masters.EquipmentTable;
			// 	int num5 = 0;
			// 	int num6 = 0;
			// 	if (num6 >= typeof(TableBase<>).TypeHandle)
			// 	{
			// 		break;
			// 	}
			// 	num5++;
			// 	EquipmentRarityFlags RarityFlags2 = byId.RarityFlags;
			// 	EquipmentEvolutionMB equipmentEvolutionMB;
			// 	if (equipmentEvolutionMB == 0)
			// 	{
			// 		break;
			// 	}
			// 	IReadOnlyListEquipmentEvolutionInfo> <EquipmentEvolutionInfoList = equipmentEvolutionMB.EquipmentEvolutionInfoList;
			// 	if (equipmentEvolutionMB != 0)
			// 	{
			// 		if (equipmentEvolutionMB.Id != (long)byId.RarityFlags)
			// 		{
			// 			continue;
			// 		}
			// 		IReadOnlyListEquipmentEvolutionInfo> <EquipmentEvolutionInfoList2 = equipmentEvolutionMB.EquipmentEvolutionInfoList;
			// 	}
			// 	if ("{il2cpp array field local31->}" != (ulong)0L)
			// 	{
			// 	}
			// }
			// while (num7 != (ulong)0L);
			// return;
			// IL_104:
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}
	}
}
