using System.Data;
using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Table;
using MementoMori.Ortega.Share.Utils;

namespace MementoMori.Ortega.Share.Extensions
{
	public static class BattleParameterExtension
	{
		public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(
			this UserCharacterInfo userCharacterInfo, List<UserEquipmentDtoInfo> userEquipmentDtoInfos, CharacterCollectionParameterInfo characterCollectionParameterInfo, long playerRank)
		{
			var v9 = userCharacterInfo.DeepCopy();
			var playerRankMb = Masters.PlayerRankTable.GetById(playerRank);
			CharacterMB byId = Masters.CharacterTable.GetById(v9.CharacterId);
			BaseParameterType baseParameterType = byId.JobFlags switch
			{
				JobFlags.Warrior => BaseParameterType.Muscle,
				JobFlags.Sniper => BaseParameterType.Energy,
				JobFlags.Sorcerer => BaseParameterType.Intelligence,
				_ => BaseParameterType.Health
			};

			BaseParameter baseParameter = CalcBaseParameter(byId, v9.Level, v9.SubLevel, byId.RarityFlags, v9.RarityFlags)
				.Add(GetEquipmentBaseParameter(userEquipmentDtoInfos));

			var baseParameterChangeInfos = new List<BaseParameterChangeInfo>();
			var battleParameterChangeInfos = new List<BattleParameterChangeInfo>();
			
			var parameterOfSphere = GetParameterOfSphere(userEquipmentDtoInfos);
			baseParameterChangeInfos.AddRange(parameterOfSphere.Item1);
			battleParameterChangeInfos.AddRange(parameterOfSphere.Item2);
			
			var battleParameterListOfEquipment = GetBattleParameterListOfEquipment(userEquipmentDtoInfos);
			battleParameterChangeInfos.AddRange(battleParameterListOfEquipment);
			
			var matchlessSacredTreasureParameterList = GetMatchlessSacredTreasureParameterList(userEquipmentDtoInfos);
			battleParameterChangeInfos.AddRange(matchlessSacredTreasureParameterList);
			
			var legendSacredTreasureParameterList = GetLegendSacredTreasureParameterList(userEquipmentDtoInfos);
			battleParameterChangeInfos.AddRange(legendSacredTreasureParameterList);
			
			var setEquipmentEffect = GetSetEquipmentEffect(userCharacterInfo.CharacterId, userEquipmentDtoInfos);
			baseParameterChangeInfos.AddRange(setEquipmentEffect.Item1);
			battleParameterChangeInfos.AddRange(setEquipmentEffect.Item2);

			baseParameterChangeInfos.AddRange(characterCollectionParameterInfo.AllCharacterBaseParameterChangeInfos);
			battleParameterChangeInfos.AddRange(characterCollectionParameterInfo.AllCharacterBattleParameterChangeInfos);
			if (characterCollectionParameterInfo.EachCharacterBaseParameterChangeInfoDict.TryGetValue(userCharacterInfo.CharacterId, out var value1))
			{
				baseParameterChangeInfos.AddRange(value1);
			}

			if (characterCollectionParameterInfo.EachCharacterBattleParameterChangeInfoDict.TryGetValue(userCharacterInfo.CharacterId, out var value2))
			{
				battleParameterChangeInfos.AddRange(value2);
			}

			
			CalcBaseParameterChangeInfo(baseParameter, baseParameterChangeInfos, userCharacterInfo.Level);
			var battleParameterDict = GetBattleParameterDict(battleParameterChangeInfos, userCharacterInfo.Level);
			
			var battleParameter = CreateBattleParameter(byId.InitialBattleParameter.DeepCopy(), baseParameter, baseParameterType, playerRankMb, battleParameterDict.Item1, battleParameterDict.Item2);

			return (baseParameter, battleParameter); 
		}

		public static ValueTuple<BaseParameter, BattleParameter> CalcCharacterBattleParameter(
			UserCharacterInfo userCharacterInfo, Dictionary<string, UserEquipmentDtoInfo> userEquipmentDtoDict, CharacterCollectionParameterInfo characterCollectionParameterInfo, long playerRank)
		{
			var userEquipmentDtoInfos = new List<UserEquipmentDtoInfo>();
			foreach (var userEquipmentDtoInfo in userEquipmentDtoDict)
			{
				if (userEquipmentDtoInfo.Key == userCharacterInfo.Guid)
				{
					userEquipmentDtoInfos.Add(userEquipmentDtoInfo.Value);
				}
			}

			return CalcCharacterBattleParameter(userCharacterInfo, userEquipmentDtoInfos, characterCollectionParameterInfo, playerRank);
		}

		public static Dictionary<ElementType, List<BattleParameterChangeInfo>> CalcElementParameterBonus(List<CharacterMB> characterMBs, int deckCharacterCount)
		{
			throw new RowNotInTableException();
			// int num;
			// do
			// {
			// 	num = 0;
			// 	Dictionary<ElementType, List<BattleParameterChangeInfo>> dictionary = new Dictionary();
			// 	Dictionary<ElementType, int> dictionary2 = new Dictionary();
			// 	bool flag;
			// 	bool flag2;
			// 	if (!flag || !flag2)
			// 	{
			// 	}
			// }
			// while (num != 0);
			// throw new NullReferenceException();
		}

		public static void GetParameterBonusResult(BattleParameter battleParameter, List<BattleParameterChangeInfo> battleParameterChangeInfo, long characterLevel)
		{
			// int num = battleParameter.Speed;
			// long num2;
			// num = (int)((long)num + num2);
			// battleParameter.Speed = (int)num2;
			// long num3;
			// battleParameter.HP = num3;
			// long num4;
			// num3 = num4;
			// battleParameter.AttackPower = num4;
			// long num5;
			// num3 = num5;
			// battleParameter.Defense = num5;
			// long num6;
			// num3 = num6;
			// battleParameter.PhysicalDamageRelax = num6;
			// long num7;
			// num3 = num7;
			// battleParameter.MagicDamageRelax = num7;
			// long num8;
			// num3 = num8;
			// battleParameter.DefensePenetration = num8;
			// long num9;
			// num3 = num9;
			// int num10 = battleParameter.Hit;
			// battleParameter.DamageEnhance = num9;
			// long num11;
			// num3 = num11;
			// num10 = (int)((long)num10 + num3);
			// int num12 = battleParameter.Avoidance;
			// battleParameter.Hit = (int)num11;
			// long num13;
			// num3 = num13;
			// num12 = (int)((long)num12 + num3);
			// int num14 = battleParameter.Critical;
			// battleParameter.Avoidance = (int)num13;
			// long num15;
			// num3 = num15;
			// num14 = (int)((long)num14 + num3);
			// int num16 = battleParameter.CriticalResist;
			// battleParameter.Critical = (int)num15;
			// long num17;
			// num3 = num17;
			// num16 = (int)((long)num16 + num3);
			// int num18 = battleParameter.HpDrain;
			// battleParameter.CriticalResist = (int)num17;
			// long num19;
			// num3 = num19;
			// num18 = (int)((long)num18 + num3);
			// battleParameter.HpDrain = (int)num19;
			// long num20;
			// num3 = num20;
			// int num21 = battleParameter.PhysicalCriticalDamageRelax;
			// battleParameter.CriticalDamageEnhance = num20;
			// long num22;
			// num3 = num22;
			// num21 = (int)((long)num21 + num3);
			// int num23 = battleParameter.MagicCriticalDamageRelax;
			// battleParameter.PhysicalCriticalDamageRelax = (int)num22;
			// long num24;
			// num3 = num24;
			// num23 = (int)((long)num23 + num3);
			// int num25 = battleParameter.DebuffHit;
			// battleParameter.MagicCriticalDamageRelax = (int)num24;
			// long num26;
			// num3 = num26;
			// num25 = (int)((long)num25 + num3);
			// int num27 = battleParameter.DebuffResist;
			// battleParameter.DebuffHit = (int)num26;
			// long num28;
			// num3 = num28;
			// num27 = (int)((long)num27 + num3);
			// int num29 = battleParameter.DamageReflect;
			// battleParameter.DebuffResist = (int)num28;
			// long num30;
			// num3 = num30;
			// num29 = (int)((long)num29 + num3);
			// battleParameter.DamageReflect = (int)num30;
		}

		public static CharacterCollectionParameterInfo CalcCharacterCollectionParameterInfo(this List<UserCharacterCollectionDtoInfo> userCharacterCollectionDtoInfos)
		{
			CharacterCollectionParameterInfo characterCollectionParameterInfo = new CharacterCollectionParameterInfo();
			characterCollectionParameterInfo.Init();
			characterCollectionParameterInfo.Setup(userCharacterCollectionDtoInfos);
			return characterCollectionParameterInfo;
		}

		public static BattleParameter CreateBattleParameter(
			BattleParameter defaultParameter, 
			BaseParameter baseParameter, 
			BaseParameterType mainParameterType,
			PlayerRankMB playerRankMB, 
			Dictionary<BattleParameterType, long> addParameterMap, 
			Dictionary<BattleParameterType, double> mulParameterMap)
		{
			BattleParameter battleParameter = new BattleParameter();
			
			battleParameter.Speed = defaultParameter.Speed + (int)addParameterMap[BattleParameterType.Speed] + playerRankMB.SpeedBonus;
			
			battleParameter.HP = (long) ((defaultParameter.HP + playerRankMB.HpBonus + addParameterMap[BattleParameterType.Hp] + 10 * baseParameter.Health)
			                             * (playerRankMB.HpPercentBonus + mulParameterMap[BattleParameterType.Hp] + 10000.0) * 0.0001);
			
			battleParameter.AttackPower = playerRankMB.AttackPowerBonus + (long)((defaultParameter.AttackPower + addParameterMap[BattleParameterType.AttackPower] + baseParameter.GetValue(mainParameterType))
				                              * (mulParameterMap[BattleParameterType.AttackPower] + 10000.0) * 0.0001);
			
			battleParameter.Defense = (long) ((mulParameterMap[BattleParameterType.Defense] + 10000) * addParameterMap[BattleParameterType.Defense] * 0.0001 + defaultParameter.Defense);
			
			battleParameter.PhysicalDamageRelax = (long) ((defaultParameter.PhysicalDamageRelax + addParameterMap[BattleParameterType.PhysicalDamageRelax] + baseParameter.Muscle)
			                                              * (mulParameterMap[BattleParameterType.PhysicalDamageRelax] + 10000) * 0.0001);
			
			battleParameter.MagicDamageRelax = (long) ((defaultParameter.MagicDamageRelax + addParameterMap[BattleParameterType.MagicDamageRelax] + baseParameter.Intelligence)
			                                           * (mulParameterMap[BattleParameterType.MagicDamageRelax] + 10000) * 0.0001);
			
			battleParameter.DefensePenetration = addParameterMap[BattleParameterType.DefensePenetration] + playerRankMB.DefensePenetrationBonus + defaultParameter.DefensePenetration;
			
			battleParameter.DamageEnhance = addParameterMap[BattleParameterType.DamageEnhance] + playerRankMB.DamageEnhanceBonus + defaultParameter.DamageEnhance;
			
			battleParameter.Hit = (int)((baseParameter.Muscle * 0.5 + defaultParameter.Hit + addParameterMap[BattleParameterType.Hit] + playerRankMB.HitBonus) 
			                            * (mulParameterMap[BattleParameterType.Hit] + 10000) * 0.0001);
			
			battleParameter.Avoidance = (int) ((baseParameter.Energy * 0.5 + defaultParameter.Avoidance + addParameterMap[BattleParameterType.Avoidance]) 
			                                   * (mulParameterMap[BattleParameterType.Avoidance] + 10000) * 0.0001);
			
			battleParameter.Critical = (int) ((baseParameter.Energy * 0.5 + defaultParameter.Critical + addParameterMap[BattleParameterType.Critical] + playerRankMB.CriticalBonus)
			                                  * (mulParameterMap[BattleParameterType.Critical] + 10000) * 0.0001);

			battleParameter.CriticalResist = (int) ((baseParameter.Health * 0.5 + defaultParameter.CriticalResist + addParameterMap[BattleParameterType.CriticalResist])
			                                        * (mulParameterMap[BattleParameterType.CriticalResist] + 10000) * 0.0001);
			
			battleParameter.HpDrain = defaultParameter.HpDrain + (int) addParameterMap[BattleParameterType.HpDrain] + playerRankMB.HpDrainBonus;
			
			battleParameter.DamageReflect =defaultParameter.DamageReflect + (int) addParameterMap[BattleParameterType.DamageReflect] + playerRankMB.DamageReflectBonus;
			
			battleParameter.CriticalDamageEnhance = defaultParameter.CriticalDamageEnhance + addParameterMap[BattleParameterType.CriticalDamageEnhance] + playerRankMB.CriticalDamageEnhanceBonus;
			
			battleParameter.PhysicalCriticalDamageRelax = (int) defaultParameter.CriticalDamageEnhance + (int) addParameterMap[BattleParameterType.PhysicalCriticalDamageRelax];
			
			battleParameter.MagicCriticalDamageRelax = (int) defaultParameter.CriticalDamageEnhance + (int) addParameterMap[BattleParameterType.MagicCriticalDamageRelax];
			
			battleParameter.DebuffHit = (int) ((baseParameter.Intelligence * 0.5 + defaultParameter.DebuffHit + addParameterMap[BattleParameterType.DebuffHit] + playerRankMB.DebuffHitBonus)
			                                   * (mulParameterMap[BattleParameterType.DebuffHit] + 10000) * 0.0001);
			battleParameter.DebuffResist = (int) (addParameterMap[BattleParameterType.DebuffResist] * (mulParameterMap[BattleParameterType.DebuffResist] + 10000) * 0.0001);
			return battleParameter;
		}

		public static BaseParameter CalcBaseParameter(CharacterMB characterMB, long level, int subLevel, CharacterRarityFlags initialRarityFlags, CharacterRarityFlags nowRarityFlags)
		{
			var v8 = level;
			var characterPotentialMb = Masters.CharacterPotentialTable.GetByLevelAndSubLevel(level, subLevel);
			var characterPotentialCoefficientMb = Masters.CharacterPotentialCoefficientTable.GetByInitialRarityAndNowRarity(initialRarityFlags, nowRarityFlags);
			
			long BaseParameterGrossCoefficient = characterMB.BaseParameterGrossCoefficient;
			if (BaseParameterGrossCoefficient == 0)
			{
				BaseParameterGrossCoefficient = characterMB.BaseParameterCoefficient.GetTotal();
				if (BaseParameterGrossCoefficient == 0)
				{
					BaseParameterGrossCoefficient = 1;
				}
			}

			var v25 = characterPotentialMb.TotalBaseParameter * characterPotentialCoefficientMb.RarityCoefficientInfo.m + characterPotentialCoefficientMb.RarityCoefficientInfo.b; 
			BaseParameter baseParameter = new BaseParameter();
			baseParameter.Energy = (long) (characterMB.BaseParameterCoefficient.Energy * v25 / BaseParameterGrossCoefficient);
			baseParameter.Health = (long) (characterMB.BaseParameterCoefficient.Health  * v25 / BaseParameterGrossCoefficient);
			baseParameter.Intelligence = (long) (characterMB.BaseParameterCoefficient.Intelligence  * v25 / BaseParameterGrossCoefficient);
			baseParameter.Muscle = (long) (characterMB.BaseParameterCoefficient.Muscle  * v25 / BaseParameterGrossCoefficient);
			return baseParameter;
		}

		public static ValueTuple<List<BaseParameterChangeInfo>, List<BattleParameterChangeInfo>> GetCharacterCollectionEffect(long characterId, CharacterCollectionParameterInfo characterCollectionParameterInfo)
		{
			throw new RowNotInTableException();
			// int num = 0;
			// List<BaseParameterChangeInfo> list = new List();
			// List<BattleParameterChangeInfo> list2 = new List();
			// bool flag;
			// if (flag)
			// {
			// 	list.AddRange(num);
			// }
			// bool flag2;
			// if (flag2)
			// {
			// 	list2.AddRange(num);
			// }
			// int num2 = 0;
			// characterId.m_value = (long)num2;
			// throw new NullReferenceException();
		}

		private static void CalcBaseParameterChangeInfo(BaseParameter baseParameter, List<BaseParameterChangeInfo> changeInfoList, long characterLevel)
		{
			int v4 = (int) characterLevel;
			if (changeInfoList != null && changeInfoList.Count > 0)
			{
				foreach (BaseParameterChangeInfo changeInfo in changeInfoList)
				{
					if (changeInfo.ChangeParameterType == ChangeParameterType.Addition)
					{
						var value = baseParameter.GetValue(changeInfo.BaseParameterType);
						baseParameter.SetValue(changeInfo.BaseParameterType, (long) (value + changeInfo.Value));
					}
					else if (changeInfo.ChangeParameterType == ChangeParameterType.AdditionPercent)
					{
						var value = baseParameter.GetValue(changeInfo.BaseParameterType);
						baseParameter.SetValue(changeInfo.BaseParameterType, (long) Math.Truncate(value + value * changeInfo.Value * 0.0001));
					}
					else if (changeInfo.ChangeParameterType == ChangeParameterType.CharacterLevelConstantMultiplicationAddition)
					{
						var value = baseParameter.GetValue(changeInfo.BaseParameterType);
						baseParameter.SetValue(changeInfo.BaseParameterType, (long) (value + characterLevel * changeInfo.Value));

					}
				}
			}
		}

		private static ValueTuple<Dictionary<BattleParameterType, long>, Dictionary<BattleParameterType, double>> GetBattleParameterDict(
			List<BattleParameterChangeInfo> infoList, long characterLevel)
		{
			var addParameters = new Dictionary<BattleParameterType, long>();
			var mulParameters = new Dictionary<BattleParameterType, double>();

			var battleParameterTypes = EnumUtil.GetValueList<BattleParameterType>();
			foreach (var battleParameterType in battleParameterTypes)
			{
				addParameters[battleParameterType] = 0;
				mulParameters[battleParameterType] = 0.0;
			}
			
			foreach (var battleParameterChangeInfo in infoList)
			{
				switch (battleParameterChangeInfo.ChangeParameterType)
				{
					case ChangeParameterType.Addition:
						addParameters[battleParameterChangeInfo.BattleParameterType] += (long)battleParameterChangeInfo.Value; 
						break;
					case ChangeParameterType.AdditionPercent:
						mulParameters[battleParameterChangeInfo.BattleParameterType] += battleParameterChangeInfo.Value;
						break;
					case ChangeParameterType.CharacterLevelConstantMultiplicationAddition:
						addParameters[battleParameterChangeInfo.BattleParameterType] += (long) battleParameterChangeInfo.Value * characterLevel;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			return (addParameters, mulParameters);
		}

		private static List<BattleParameterChangeInfo> GetBattleParameterListOfEquipment(List<UserEquipmentDtoInfo> equipmentInfoList)
		{
			List<BattleParameterChangeInfo> list = new List<BattleParameterChangeInfo>();
			
			foreach (var userEquipmentDtoInfo in equipmentInfoList)
			{
				var equipmentMb = Masters.EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId);
				var equipmentReinforcementParameterMb = Masters.EquipmentReinforcementParameterTable.GetById(userEquipmentDtoInfo.ReinforcementLv);
				var ratio = equipmentReinforcementParameterMb?.ReinforcementCoefficient ?? 1.0;
				list.Add(new BattleParameterChangeInfo()
				{
					BattleParameterType = equipmentMb.BattleParameterChangeInfo.BattleParameterType,
					ChangeParameterType = equipmentMb.BattleParameterChangeInfo.ChangeParameterType,
					Value = equipmentMb.BattleParameterChangeInfo.Value * ratio
				});
			}
			return list;
		}

		private static List<BattleParameterChangeInfo> GetLegendSacredTreasureParameterList(List<UserEquipmentDtoInfo> equipmentInfoList)
		{
			List<BattleParameterChangeInfo> list = new List<BattleParameterChangeInfo>();
			foreach (var userEquipmentDtoInfo in equipmentInfoList)
			{
				if (userEquipmentDtoInfo.LegendSacredTreasureLv > 0)
				{
					var equipmentMb = Masters.EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId);
					var equipmentLegendSacredTreasureMb = Masters.EquipmentLegendSacredTreasureTable.GetByLevel(userEquipmentDtoInfo.LegendSacredTreasureLv);
					list.Add(equipmentLegendSacredTreasureMb.GetValue(equipmentMb.SlotType));	
				}
			}

			return list;
		}

		private static List<BattleParameterChangeInfo> GetMatchlessSacredTreasureParameterList(List<UserEquipmentDtoInfo> equipmentInfoList)
		{
			List<BattleParameterChangeInfo> list = new List<BattleParameterChangeInfo>();
			foreach (var userEquipmentDtoInfo in equipmentInfoList)
			{
				if (userEquipmentDtoInfo.MatchlessSacredTreasureLv > 0)
				{
					var equipmentMb = Masters.EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId);
					var equipmentMatchlessSacredTreasureMb = Masters.EquipmentMatchlessSacredTreasureTable.GetByLevel(userEquipmentDtoInfo.MatchlessSacredTreasureLv);
					list.Add(equipmentMatchlessSacredTreasureMb.GetValue(equipmentMb.SlotType));					
				}
			}
			return list;
		}

		private static ValueTuple<List<BaseParameterChangeInfo>, List<BattleParameterChangeInfo>> GetParameterOfSphere(
			List<UserEquipmentDtoInfo> equipmentList)
		{
			var baseParameterChangeInfos = new List<BaseParameterChangeInfo>();
			var battleParameterChangeInfos = new List<BattleParameterChangeInfo>();
			foreach (var userEquipmentDtoInfo in equipmentList)
			{
				if (userEquipmentDtoInfo.SphereId1 > 0)
				{
					var sphereMb = Masters.SphereTable.GetById(userEquipmentDtoInfo.SphereId1);
					if (sphereMb.BaseParameterChangeInfo != null) baseParameterChangeInfos.Add(sphereMb.BaseParameterChangeInfo);
					if (sphereMb.BattleParameterChangeInfo != null) battleParameterChangeInfos.Add(sphereMb.BattleParameterChangeInfo);
				}
				if (userEquipmentDtoInfo.SphereId2 > 0)
				{
					var sphereMb = Masters.SphereTable.GetById(userEquipmentDtoInfo.SphereId2);
					if (sphereMb.BaseParameterChangeInfo != null) baseParameterChangeInfos.Add(sphereMb.BaseParameterChangeInfo);
					if (sphereMb.BattleParameterChangeInfo != null) battleParameterChangeInfos.Add(sphereMb.BattleParameterChangeInfo);
				}
				if (userEquipmentDtoInfo.SphereId3 > 0)
				{
					var sphereMb = Masters.SphereTable.GetById(userEquipmentDtoInfo.SphereId3);
					if (sphereMb.BaseParameterChangeInfo != null) baseParameterChangeInfos.Add(sphereMb.BaseParameterChangeInfo);
					if (sphereMb.BattleParameterChangeInfo != null) battleParameterChangeInfos.Add(sphereMb.BattleParameterChangeInfo);
				}
				if (userEquipmentDtoInfo.SphereId4 > 0)
				{
					var sphereMb = Masters.SphereTable.GetById(userEquipmentDtoInfo.SphereId4);
					if (sphereMb.BaseParameterChangeInfo != null) baseParameterChangeInfos.Add(sphereMb.BaseParameterChangeInfo);
					if (sphereMb.BattleParameterChangeInfo != null) battleParameterChangeInfos.Add(sphereMb.BattleParameterChangeInfo);
				}
			}

			return (baseParameterChangeInfos, battleParameterChangeInfos);
		}

		private static ValueTuple<List<BaseParameterChangeInfo>, List<BattleParameterChangeInfo>> GetSetEquipmentEffect(
			long characterId, List<UserEquipmentDtoInfo> userEquipmentDtoInfos)
		{
			var baseParameterChangeInfos = new List<BaseParameterChangeInfo>();
			var battleParameterChangeInfos = new List<BattleParameterChangeInfo>();
			var equipmentMbs = new List<EquipmentMB>();
			var equipmentSetCount = new Dictionary<long, int>();
			foreach (var userEquipmentDtoInfo in userEquipmentDtoInfos)
			{
				var equipmentMb = Masters.EquipmentTable.GetById(userEquipmentDtoInfo.EquipmentId);
				equipmentMbs.Add(equipmentMb);

				if (equipmentMb.EquipmentSetId > 0)
				{
					if (equipmentSetCount.ContainsKey(equipmentMb.EquipmentSetId))
					{
						equipmentSetCount[equipmentMb.EquipmentSetId] ++;
					}
					else
					{
						equipmentSetCount[equipmentMb.EquipmentSetId] = 1;
					}

					// foreach (var mb in equipmentMbs)
					// {
					// 	if (equipmentSetCount.ContainsKey(mb.EquipmentSetId))
					// 	{
					// 		equipmentSetCount[mb.EquipmentSetId] ++;
					// 	}
					// 	else
					// 	{
					// 		equipmentSetCount[mb.EquipmentSetId] = 1;
					// 	}
					// }
				}

				if (equipmentMb.ExclusiveEffectId > 0)
				{
					var equipmentExclusiveEffectMb = Masters.EquipmentExclusiveEffectTable.GetByIdAndCharacterId(equipmentMb.ExclusiveEffectId, characterId);
					if (equipmentExclusiveEffectMb != null)
					{
						if (!equipmentExclusiveEffectMb.BaseParameterChangeInfoList.IsNullOrEmpty())
						{
							baseParameterChangeInfos.AddRange(equipmentExclusiveEffectMb.BaseParameterChangeInfoList);
						}

						if (!equipmentExclusiveEffectMb.BattleParameterChangeInfoList.IsNullOrEmpty())
						{
							battleParameterChangeInfos.AddRange(equipmentExclusiveEffectMb.BattleParameterChangeInfoList);
						}
					}
				}
			}
			
			foreach (var keyValuePair in equipmentSetCount)
			{
				var equipmentSetMb = Masters.EquipmentSetTable.GetById(keyValuePair.Key);
				foreach (var equipmentSetEffect in equipmentSetMb.EffectList)
				{
					if (keyValuePair.Value >= equipmentSetEffect.RequiredEquipmentCount)
					{
						if (equipmentSetEffect.BaseParameterChangeInfo != null) baseParameterChangeInfos.Add(equipmentSetEffect.BaseParameterChangeInfo);
						if (equipmentSetEffect.BattleParameterChangeInfo != null) battleParameterChangeInfos.Add(equipmentSetEffect.BattleParameterChangeInfo);
					}
				}
			}

			return (baseParameterChangeInfos, battleParameterChangeInfos);
		}

		private static BaseParameter GetEquipmentBaseParameter(List<UserEquipmentDtoInfo> equipmentInfoList)
		{
			BaseParameter baseParameter = new BaseParameter();
			foreach (var userEquipmentDtoInfo in equipmentInfoList)
			{
				baseParameter.Muscle += userEquipmentDtoInfo.AdditionalParameterMuscle;
				baseParameter.Health += userEquipmentDtoInfo.AdditionalParameterHealth;
				baseParameter.Energy += userEquipmentDtoInfo.AdditionalParameterEnergy;
				baseParameter.Intelligence += userEquipmentDtoInfo.AdditionalParameterIntelligence;
			}
			return baseParameter;
		}
	}
}
