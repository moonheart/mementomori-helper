using System.Text;
using MementoMori.Common.Localization;
using MementoMori.Ortega.Common.Data;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.TreatureChest;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.Ortega.Common.Utils
{
    public static class ItemUtil
    {
        public static string GetItemName(ItemType itemType, long itemId)
        {
            switch (itemType)
            {
                case ItemType.None:
                    break;
                case ItemType.CurrencyFree:
                case ItemType.CurrencyPaid:
                case ItemType.Gold:
                case ItemType.QuestQuickTicket:
                case ItemType.CharacterTrainingMaterial:
                case ItemType.EquipmentReinforcementItem:
                case ItemType.ExchangePlaceItem:
                case ItemType.MatchlessSacredTreasureExpItem:
                case ItemType.GachaTicket:
                case ItemType.TreasureChestKey:
                case ItemType.BossChallengeTicket:
                case ItemType.TowerBattleTicket:
                case ItemType.DungeonRecoveryItem:
                case ItemType.PlayerExp:
                case ItemType.FriendPoint:
                case ItemType.EquipmentRarityCrystal:
                case ItemType.GuildFame:
                case ItemType.GuildExp:
                case ItemType.ActivityMedal:
                case ItemType.EventExchangePlaceItem:
                case ItemType.PanelGetJudgmentItem:
                case ItemType.UnlockPanelGridItem:
                case ItemType.PanelUnlockItem:
                case ItemType.MusicTicket:
                    var itemMb = Masters.ItemTable.GetByItemTypeAndItemId(itemType, itemId);
                    return Masters.TextResourceTable.Get(itemMb.NameKey);
                case ItemType.Equipment:
                {
                    var equipmentMb = Masters.EquipmentTable.GetById(itemId);
                    return Masters.TextResourceTable.Get(equipmentMb.NameKey);
                }
                case ItemType.EquipmentFragment:
                {
                    var equipmentCompositeMb = Masters.EquipmentCompositeTable.GetById(itemId);
                    var equipmentMb = Masters.EquipmentTable.GetById(equipmentCompositeMb.EquipmentId);
                    var name = Masters.TextResourceTable.Get(equipmentMb.NameKey);
                    return Masters.TextResourceTable.Get("[CommonItemEquipmentFragmentFormat]", name);
                }
                case ItemType.Character:
                {
                    var characterMb = Masters.CharacterTable.GetById(itemId);
                    var combinedName = characterMb.GetCombinedName();
                    return combinedName;
                }
                case ItemType.CharacterFragment:
                {
                    var characterMb = Masters.CharacterTable.GetById(itemId);
                    var combinedName = characterMb.GetCombinedName();
                    return Masters.TextResourceTable.Get("[CommonItemCharacterFragment]", combinedName);
                    break;
                }
                case ItemType.DungeonBattleRelic:
                    var dungeonBattleRelicMb = Masters.DungeonBattleRelicTable.GetById(itemId);
                    return Masters.TextResourceTable.Get(dungeonBattleRelicMb.NameKey);
                    break;
                case ItemType.EquipmentSetMaterial:
                    var equipmentSetMaterialMb = Masters.EquipmentSetMaterialTable.GetById(itemId);
                    return Masters.TextResourceTable.Get(equipmentSetMaterialMb.NameKey);
                    break;
                case ItemType.Sphere:
                    var sphereMb = Masters.SphereTable.GetById(itemId);
                    return Masters.TextResourceTable.Get(sphereMb.NameKey);
                    break;
                case ItemType.TreasureChest:
                    var treasureChestMb = Masters.TreasureChestTable.GetById(itemId);
                    return Masters.TextResourceTable.Get(treasureChestMb.NameKey);
                    break;
                case ItemType.LevelLinkExp:
                    var levelLinkExpMb = Masters.LevelLinkTable.GetById(itemId);
                    return levelLinkExpMb.Memo;
                    break;
            }

            return ResourceStrings.Unknown;

            // if ("[CommonItemEquipmentFragmentFormat]" > (ulong)13L)
            // {
            // 	ItemTable ItemTable = Masters.ItemTable;
            // 	ItemMB itemMB;
            // 	while (itemMB == 0)
            // 	{
            // 	}
            // 	TextResourceTable TextResourceTable = Masters.TextResourceTable;
            // 	throw new NullReferenceException();
            // }
            // EquipmentMB equipmentMB;
            // if (equipmentMB != 0)
            // {
            // 	string NameKey = equipmentMB.NameKey;
            // 	string text;
            // 	return text;
            // }
            // return string.Empty;
        }

        public static string GetItemRarity(ItemType itemType, long itemId)
        {
            switch (itemType)
            {
                case ItemType.None:
                    break;
                case ItemType.CurrencyFree:
                case ItemType.CurrencyPaid:
                case ItemType.Gold:
                case ItemType.QuestQuickTicket:
                case ItemType.CharacterTrainingMaterial:
                case ItemType.EquipmentReinforcementItem:
                case ItemType.ExchangePlaceItem:
                case ItemType.MatchlessSacredTreasureExpItem:
                case ItemType.GachaTicket:
                case ItemType.TreasureChestKey:
                case ItemType.BossChallengeTicket:
                case ItemType.TowerBattleTicket:
                case ItemType.DungeonRecoveryItem:
                case ItemType.PlayerExp:
                case ItemType.FriendPoint:
                case ItemType.EquipmentRarityCrystal:
                case ItemType.GuildFame:
                case ItemType.GuildExp:
                case ItemType.ActivityMedal:
                case ItemType.EventExchangePlaceItem:
                case ItemType.PanelGetJudgmentItem:
                case ItemType.UnlockPanelGridItem:
                case ItemType.PanelUnlockItem:
                case ItemType.MusicTicket:
                    var itemMb = Masters.ItemTable.GetByItemTypeAndItemId(itemType, itemId);
                    return itemMb.ItemRarityFlags.ToString();
                case ItemType.Equipment:
                    var equipmentMb = Masters.EquipmentTable.GetById(itemId);
                    return equipmentMb.RarityFlags.ToString();
                case ItemType.EquipmentFragment:
                    var equipmentFragmentMb = Masters.EquipmentTable.GetById(itemId);
                    return equipmentFragmentMb.RarityFlags.ToString();
                    break;
                case ItemType.Character:
                    var characterMb = Masters.CharacterTable.GetById(itemId);
                    return characterMb.RarityFlags.ToString();
                    break;
                case ItemType.CharacterFragment:
                    var characterFragmentMb = Masters.CharacterTable.GetById(itemId);
                    return characterFragmentMb.RarityFlags.ToString();
                    break;
                case ItemType.DungeonBattleRelic:
                    var dungeonBattleRelicMb = Masters.DungeonBattleRelicTable.GetById(itemId);
                    return dungeonBattleRelicMb.DungeonRelicRarityType.ToString();
                    break;
                case ItemType.EquipmentSetMaterial:
                    var equipmentSetMaterialMb = Masters.EquipmentSetMaterialTable.GetById(itemId);
                    return equipmentSetMaterialMb.ItemRarityFlags.ToString();
                    break;
                case ItemType.Sphere:
                    var sphereMb = Masters.SphereTable.GetById(itemId);
                    return sphereMb.RarityFlags.ToString();
                    break;
                case ItemType.TreasureChest:
                    var treasureChestMb = Masters.TreasureChestTable.GetById(itemId);
                    return treasureChestMb.ItemRarityFlags.ToString();
                    break;
                case ItemType.LevelLinkExp:
                    var levelLinkExpMb = Masters.LevelLinkTable.GetById(itemId);
                    return "None";
                    break;
            }

            return ResourceStrings.Unknown;
        }

        public static string GetItemDescription(ItemType itemType, long itemId)
        {
            // object[] array2;
            // EquipmentCompositeMB equipmentCompositeMB;
            // object[] array4;
            // string text8;
            // for (;;)
            // {
            // 	if ("[CommonAlternativesLabel]" != 0)
            // 	{
            // 		if ("[CommonAlternativesLabel]" != 0)
            // 		{
            // 			if ("[CommonAlternativesLabel]" != 0)
            // 			{
            // 				if ("[CommonAlternativesLabel]" != 0)
            // 				{
            // 					if ("[CommonAlternativesLabel]" != (ulong)1L)
            // 					{
            // 						if (itemType == ItemType.Sphere)
            // 						{
            // 							SphereMB sphereMB;
            // 							if (sphereMB == 0)
            // 							{
            // 								goto IL_2E4;
            // 							}
            // 							string DescriptionKey = sphereMB.DescriptionKey;
            // 						}
            // 						if (itemType != ItemType.TreasureChest)
            // 						{
            // 							goto IL_264;
            // 						}
            // 						TreasureChestMB treasureChestMB;
            // 						if (treasureChestMB == 0)
            // 						{
            // 							goto IL_2E4;
            // 						}
            // 						string <DescriptionKey>k__BackingField2 = treasureChestMB.DescriptionKey;
            // 						TreasureChestCeilingTable TreasureChestCeilingTable = Masters.TreasureChestCeilingTable;
            // 						IReadOnlyList<long> TreasureChestItemIdList = treasureChestMB.TreasureChestItemIdList;
            // 						TreasureChestCeilingMB treasureChestCeilingMB;
            // 						if (treasureChestCeilingMB == 0)
            // 						{
            // 							IReadOnlyList<long> <TreasureChestItemIdList>k__BackingField2 = treasureChestMB.TreasureChestItemIdList;
            // 							TreasureChestItemMB treasureChestItemMB;
            // 							if (treasureChestItemMB != 0 && treasureChestItemMB.TreasureChestItemListType == TreasureChestItemListType.SelectItemList)
            // 							{
            // 								StringBuilder stringBuilder = new StringBuilder();
            // 								string text;
            // 								StringBuilder stringBuilder2 = stringBuilder.AppendLine(text);
            // 								StringBuilder stringBuilder3 = stringBuilder.AppendLine();
            // 								string text2;
            // 								StringBuilder stringBuilder4 = stringBuilder.Append(text2);
            // 								IReadOnlyList<TreasureChestSelectItem> SelectItemList = treasureChestItemMB.SelectItemList;
            // 								int num = 0;
            // 								if (stringBuilder4 != 0)
            // 								{
            // 									uint num2;
            // 									if (num >= (int)num2)
            // 									{
            // 										goto IL_F0;
            // 									}
            // 									num += num;
            // 									if (num != (int)num2)
            // 									{
            // 										num++;
            // 										goto IL_F0;
            // 									}
            // 									goto IL_115;
            // 									IL_11C:
            // 									object[] array;
            // 									string text3;
            // 									array[0] = text3;
            // 									TextResourceTable TextResourceTable;
            // 									if (array == 0 || array != 0)
            // 									{
            // 										array[1] = array;
            // 										string text4 = TextResourceTable.Get("[CommonAlternativesItemFormat]", array);
            // 										StringBuilder stringBuilder5 = stringBuilder.Append(text4);
            // 										goto IL_158;
            // 									}
            // 									goto IL_2F0;
            // 									IL_115:
            // 									text3 += text3;
            // 									goto IL_11C;
            // 									IL_F0:
            // 									StringBuilder stringBuilder6 = stringBuilder.AppendLine();
            // 									TextResourceTable = Masters.TextResourceTable;
            // 									array = new object[2];
            // 									if (text3 == 0)
            // 									{
            // 										goto IL_11C;
            // 									}
            // 									if (array != 0)
            // 									{
            // 										goto IL_115;
            // 									}
            // 									goto IL_2EA;
            // 								}
            // 								IL_158:
            // 								if ("{il2cpp array field local40->}" != (ulong)0L)
            // 								{
            // 								}
            // 								if (num != 0)
            // 								{
            // 									continue;
            // 								}
            // 								string text5 = stringBuilder.ToString();
            // 							}
            // 						}
            // 						Dictionary<long, long> TreasureChestCeilingCountMap = SingletonMonoBehaviour.Instance.SyncData.TreasureChestCeilingCountMap;
            // 						long TreasureChestItemId = treasureChestCeilingMB.TreasureChestItemId;
            // 						if (TreasureChestCeilingCountMap.ContainsKey(TreasureChestItemId))
            // 						{
            // 							Dictionary<long, long> <TreasureChestCeilingCountMap>k__BackingField2 = SingletonMonoBehaviour.Instance.SyncData.TreasureChestCeilingCountMap;
            // 							long <TreasureChestItemId>k__BackingField2 = treasureChestCeilingMB.TreasureChestItemId;
            // 							long num3 = <TreasureChestCeilingCountMap>k__BackingField2[<TreasureChestItemId>k__BackingField2];
            // 						}
            // 						string <DescriptionKey>k__BackingField3 = treasureChestMB.DescriptionKey;
            // 						array2 = new object[2];
            // 						int CeilingCount = treasureChestCeilingMB.CeilingCount;
            // 						if (array2 == 0 || array2 != 0)
            // 						{
            // 							break;
            // 						}
            // 						continue;
            // 					}
            // 					else
            // 					{
            // 						EquipmentSetMaterialMB equipmentSetMaterialMB;
            // 						if (equipmentSetMaterialMB == 0)
            // 						{
            // 							goto IL_2E4;
            // 						}
            // 						string <DescriptionKey>k__BackingField4 = equipmentSetMaterialMB.DescriptionKey;
            // 					}
            // 				}
            // 				DungeonBattleRelicMB dungeonBattleRelicMB;
            // 				if (dungeonBattleRelicMB == 0)
            // 				{
            // 					goto IL_2E4;
            // 				}
            // 				string <DescriptionKey>k__BackingField5 = dungeonBattleRelicMB.DescriptionKey;
            // 			}
            // 			CharacterMB characterMB;
            // 			if (characterMB == 0)
            // 			{
            // 				goto IL_2E4;
            // 			}
            // 			object[] array3 = new object[2];
            // 			string NameKey = characterMB.NameKey;
            // 			string text6;
            // 			if (text6 != 0 && text6 == 0)
            // 			{
            // 				continue;
            // 			}
            // 			array3[0] = text6;
            // 			long RequireFragmentCount = characterMB.RequireFragmentCount;
            // 			array3[1] = RequireFragmentCount;
            // 		}
            // 		IL_264:
            // 		ItemMB itemMB;
            // 		if (itemMB == 0)
            // 		{
            // 			goto IL_2E4;
            // 		}
            // 		TextResourceTable <TextResourceTable>k__BackingField2 = Masters.TextResourceTable;
            // 		string <DescriptionKey>k__BackingField6 = itemMB.DescriptionKey;
            // 		string text7 = <TextResourceTable>k__BackingField2.Get(<DescriptionKey>k__BackingField6);
            // 	}
            // 	if (equipmentCompositeMB == 0)
            // 	{
            // 		goto IL_2E4;
            // 	}
            // 	long EquipmentId = equipmentCompositeMB.EquipmentId;
            // 	EquipmentMB equipmentMB;
            // 	if (equipmentMB == 0)
            // 	{
            // 		goto IL_2E4;
            // 	}
            // 	string <NameKey>k__BackingField2 = equipmentMB.NameKey;
            // 	array4 = new object[3];
            // 	if (text8 == 0 || array4 != 0)
            // 	{
            // 		goto IL_2B8;
            // 	}
            // }
            // array2[0] = array2;
            // long num4;
            // array2[1] = num4;
            // string text9;
            // return text9;
            // IL_2B8:
            // array4[0] = text8;
            // long RequiredFragmentCount = equipmentCompositeMB.RequiredFragmentCount;
            // array4[1] = RequiredFragmentCount;
            // if (text8 != 0)
            // {
            // }
            // array4[2] = text8;
            // IL_2E4:
            // throw new NullReferenceException();
            // IL_2EA:
            // throw new IndexOutOfRangeException();
            // IL_2F0:
            // throw new IndexOutOfRangeException();
            throw new NotImplementedException();
        }

        public static string GetItemName(IUserItem userItem)
        {
            return GetItemName(userItem.ItemType, userItem.ItemId);
        }

        public static string GetItemDisplayName(IUserItem userItem)
        {
            switch (userItem.ItemType)
            {
                case ItemType.Equipment:
                case ItemType.EquipmentFragment:
                case ItemType.Character:
                case ItemType.CharacterFragment:
                case ItemType.DungeonBattleRelic:
                case ItemType.PlayerExp:
                case ItemType.FriendPoint:
                case ItemType.LevelLinkExp:
                case ItemType.GuildFame:
                case ItemType.GuildExp:
                case ItemType.ActivityMedal:
                    return ItemUtil.GetItemName(userItem.ItemType, userItem.ItemId);
                case ItemType.EquipmentSetMaterial:
                    var equipmentSetMaterialMb = Masters.EquipmentSetMaterialTable.GetById(userItem.ItemId);
                    return Masters.TextResourceTable.Get(equipmentSetMaterialMb.NameKey);
                case ItemType.Sphere:
                    var sphereMb = Masters.SphereTable.GetById(userItem.ItemId);
                    if (sphereMb.BaseParameterChangeInfo != null)
                    {
                        return Masters.TextResourceTable.Get(sphereMb.BaseParameterChangeInfo.BaseParameterType);
                    }

                    if (sphereMb.BattleParameterChangeInfo != null)
                    {
                        return Masters.TextResourceTable.Get(sphereMb.BattleParameterChangeInfo.BattleParameterType);
                    }

                    return string.Empty;
                case ItemType.TreasureChest:
                    var treasureChestMb = Masters.TreasureChestTable.GetById(userItem.ItemId);
                    return Masters.TextResourceTable.Get(treasureChestMb.NameKey);
                case ItemType.EquipmentSetMaterialBox:
                    var equipmentSetMaterialBoxMb = Masters.EquipmentSetMaterialBoxTable.GetById(userItem.ItemId);
                    return Masters.TextResourceTable.Get(equipmentSetMaterialBoxMb.NameKey);
                default:
                    var itemMb = Masters.ItemTable.GetByItemTypeAndItemId(userItem.ItemType, userItem.ItemId);
                    return Masters.TextResourceTable.Get(itemMb.DisplayName);
            }

            // if (typeof(string).TypeHandle > (ulong)24L)
            // {
            // 	ItemTable ItemTable = Masters.ItemTable;
            // 	ItemMB itemMB;
            // 	while (itemMB == 0)
            // 	{
            // 	}
            // 	TextResourceTable TextResourceTable = Masters.TextResourceTable;
            // 	string DisplayName = itemMB.DisplayName;
            // 	return TextResourceTable.Get(DisplayName);
            // }
            // EquipmentSetMaterialMB equipmentSetMaterialMB;
            // if (equipmentSetMaterialMB != 0)
            // {
            // 	TextResourceTable <TextResourceTable>k__BackingField2 = Masters.TextResourceTable;
            // 	string DisplayNameKey = equipmentSetMaterialMB.DisplayNameKey;
            // 	return <TextResourceTable>k__BackingField2.Get(DisplayNameKey);
            // }
            // return string.Empty;
            throw new NotImplementedException();
        }

        public static string GetItemExpirationDate(ItemType itemType, long itemId)
        {
            string text;
            // TextResourceTable <TextResourceTable>k__BackingField2;
            // object[] array2;
            // for (;;)
            // {
            // 	ItemTable ItemTable = Masters.ItemTable;
            // 	ItemMB itemMB;
            // 	string empty;
            // 	if (itemMB == 0 || itemMB.EndTime == 0)
            // 	{
            // 		empty = string.Empty;
            // 	}
            // 	if (string.IsNullOrEmpty(empty))
            // 	{
            // 		break;
            // 	}
            // 	DateTime dateTime = empty.ToDateTime();
            // 	if (itemType == ItemType.GachaTicket)
            // 	{
            // 		DateTime dateTime2 = TimeManager.Instance.ConvertJstDateTImeToLocalDateTime(dateTime);
            // 	}
            // 	TextResourceTable TextResourceTable = Masters.TextResourceTable;
            // 	object[] array = new object[5];
            // 	int num2;
            // 	int num = num2;
            // 	if (num2 == 0 || num2 != 0)
            // 	{
            // 		array[0] = num;
            // 		int num3;
            // 		num = num3;
            // 		if (num3 == 0 || num3 != 0)
            // 		{
            // 			array[1] = num;
            // 			int num4;
            // 			num = num4;
            // 			if (num4 == 0 || num4 != 0)
            // 			{
            // 				array[2] = num;
            // 				int num5;
            // 				num = num5;
            // 				if (num5 == 0 || num5 != 0)
            // 				{
            // 					array[3] = num;
            // 					int num6;
            // 					num = num6;
            // 					if (num6 == 0 || num6 != 0)
            // 					{
            // 						array[4] = num;
            // 						text = TextResourceTable.Get("[CommonDateFormat]", array);
            // 						<TextResourceTable>k__BackingField2 = Masters.TextResourceTable;
            // 						array2 = new object[1];
            // 						if (text == 0 || array2 != 0)
            // 						{
            // 							goto IL_F7;
            // 						}
            // 					}
            // 				}
            // 			}
            // 		}
            // 	}
            // }
            // return string.Empty;
            // IL_F7:
            // array2[0] = text;
            // return <TextResourceTable>k__BackingField2.Get("[ItemExpirationDateFormat]", array2);
            throw new NotImplementedException();
        }

        public static bool IsMaxSphereLevel(long sphereId)
        {
            // SphereMB byId = Masters.SphereTable.GetById(sphereId);
            // if (byId != 0)
            // {
            // 	return byId.Lv >= "{il2cpp field on {'constant30' (constant value of type Cpp2IL.Core.Analysis.ResultModels.StaticFieldsPtr)}, offset 0xX}";
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static long GetSphereLevelUpId(long sphereId)
        {
            // SphereMB byId = Masters.SphereTable.GetById(sphereId);
            // if (byId != 0)
            // {
            // 	SphereTable SphereTable = Masters.SphereTable;
            // 	long Lv = byId.Lv;
            // 	long CategoryId = byId.CategoryId;
            // 	SphereMB byCategoryIdAndLevel = SphereTable.GetByCategoryIdAndLevel(CategoryId, Lv);
            // 	if (byCategoryIdAndLevel != 0)
            // 	{
            // 		return byCategoryIdAndLevel.Id;
            // 	}
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static bool CanSphereSynthesis(long sphereId, bool isEquip)
        {
            // SphereMB byId = Masters.SphereTable.GetById(sphereId);
            // SphereTable SphereTable = Masters.SphereTable;
            // long Lv = byId.Lv;
            // long CategoryId = byId.CategoryId;
            // if (SphereTable.GetByCategoryIdAndLevel(CategoryId, Lv) != 0)
            // {
            // 	SphereSynthesisMaterialData sphereSynthesisMaterialData = new SphereSynthesisMaterialData();
            // 	bool flag;
            // 	if (flag)
            // 	{
            // 		long requiredGold = sphereSynthesisMaterialData.RequiredGold;
            // 		long gold = SingletonMonoBehaviour.Instance.Gold;
            // 		long requiredCurrency = sphereSynthesisMaterialData.RequiredCurrency;
            // 		long currency = SingletonMonoBehaviour.Instance.Currency;
            // 		return true;
            // 	}
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static bool CanSphereSynthesis(long[] sphereIds, bool isEquip)
        {
            // int num = 0;
            // if (num < sphereIds.Length)
            // {
            // 	if (!ItemUtil.CanSphereSynthesis(sphereIds[num], isEquip))
            // 	{
            // 		num++;
            // 	}
            // 	return true;
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static SphereSynthesisMaterialData GetSphereSynthesisMaterialData(SphereMB sphereMB, bool isEquip)
        {
            // if (sphereMB != 0)
            // {
            // 	SphereSynthesisMaterialData sphereSynthesisMaterialData = new SphereSynthesisMaterialData();
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static long GetQuestIdToObtainItem(long itemId)
        {
            // EquipmentSetMaterialMB byId = Masters.EquipmentSetMaterialTable.GetById(itemId);
            // if (byId != 0)
            // {
            // 	long clearedAutoBattleMaxQuestId = SingletonMonoBehaviour.Instance.GetClearedAutoBattleMaxQuestId();
            // 	IReadOnlyList<long> QuestIdList = byId.QuestIdList;
            // 	if (clearedAutoBattleMaxQuestId > clearedAutoBattleMaxQuestId)
            // 	{
            // 	}
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static bool IsShowRewardDetail(IUserItem userItem)
        {
            // if ((ulong)((uint)2) != (ulong)4294967293L)
            // {
            // 	if ("TreasureChestMBが見つかりません\u3000ID：{0}" == (ulong)17L)
            // 	{
            // 		TreasureChestMB treasureChestMB;
            // 		if (treasureChestMB != 0)
            // 		{
            // 			if (treasureChestMB.TreasureChestLotteryType != TreasureChestLotteryType.Random && treasureChestMB.TreasureChestLotteryType != TreasureChestLotteryType.SelectItem && treasureChestMB.TreasureChestLotteryType != TreasureChestLotteryType.SelectRandom && treasureChestMB.TreasureChestLotteryType != TreasureChestLotteryType.Fix)
            // 			{
            // 				return treasureChestMB.TreasureChestLotteryType == TreasureChestLotteryType.RandomFix;
            // 			}
            // 		}
            // 		else
            // 		{
            // 			string text = string.Format("TreasureChestMBが見つかりません\u3000ID：{0}", treasureChestMB);
            // 		}
            // 	}
            // }
            // return true;
            throw new NotImplementedException();
        }

        public static bool IsGettableEquipmentSetMaterial(long itemId)
        {
            // EquipmentSetMaterialMB byId = Masters.EquipmentSetMaterialTable.GetById(itemId);
            // if (byId != 0 && !byId.QuestIdList.IsNullOrEmpty<long>())
            // {
            // 	long clearedAutoBattleMaxQuestId = SingletonMonoBehaviour.Instance.GetClearedAutoBattleMaxQuestId();
            // 	return true;
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static bool IsPossibleShowingSimpleDescription(IUserItem userItem)
        {
            // return userItem != 0;
            throw new NotImplementedException();
        }

        // public static bool IsPossibleShowingSimpleDescription(ItemAcquisitionData itemAcquisitionData)
        // {
        // 	return !itemAcquisitionData.IsNullOrEmpty();
        // }

        public static bool IsDiamond(ItemType itemType)
        {
            // throw new AnalysisFailedException("CPP2IL failed to recover any usable IL for this method.");
            throw new NotImplementedException();
        }

        public static bool IsDestinyGachaTicket(IUserItem userItem)
        {
            // if (typeof(Masters).TypeHandle == (ulong)16L)
            // {
            // 	ItemTable ItemTable = Masters.ItemTable;
            // 	ItemMB itemMB;
            // 	if (itemMB != 0)
            // 	{
            // 		GachaCaseTable GachaCaseTable = Masters.GachaCaseTable;
            // 		long TransferSpotId = itemMB.TransferSpotId;
            // 		GachaCaseMB byId = GachaCaseTable.GetById(TransferSpotId);
            // 		if (byId != 0)
            // 		{
            // 			return byId.GachaSelectListType == GachaSelectListType.Destiny;
            // 		}
            // 	}
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static bool IsEquipmentGachaTicket(IUserItem userItem)
        {
            // if (typeof(Masters).TypeHandle == (ulong)16L)
            // {
            // 	ItemTable ItemTable = Masters.ItemTable;
            // 	ItemMB itemMB;
            // 	if (itemMB != 0)
            // 	{
            // 		GachaCaseTable GachaCaseTable = Masters.GachaCaseTable;
            // 		long TransferSpotId = itemMB.TransferSpotId;
            // 		GachaCaseMB byId = GachaCaseTable.GetById(TransferSpotId);
            // 		if (byId != 0)
            // 		{
            // 			return byId.GachaCategoryType == GachaCategoryType.Equipment;
            // 		}
            // 	}
            // }
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        public static bool IsInTime(IUserItem userItem)
        {
            // ItemTable ItemTable = Masters.ItemTable;
            // ItemMB itemMB;
            // if (itemMB == 0)
            // {
            // 	throw new NullReferenceException();
            // }
            // if (!string.IsNullOrEmpty(itemMB.StartTime) && !string.IsNullOrEmpty(itemMB.EndTime))
            // {
            // 	DateTime dateTime = itemMB.StartTime.ToDateTime();
            // 	DateTime dateTime2 = itemMB.EndTime.ToDateTime();
            // 	if (dateTime2 == (ulong)16L)
            // 	{
            // 	}
            // 	return TimeManager.Instance.IsInTime(dateTime, dateTime2);
            // }
            // return true;
            throw new NotImplementedException();
        }

        private static bool GetSphereSynthesisMaterialData(SphereMB sphereMB, long count, SphereSynthesisMaterialData data, bool isEquip)
        {
            // SphereMB sphereMB2;
            // int num;
            // do
            // {
            // 	SphereTable SphereTable = Masters.SphereTable;
            // 	long Lv = sphereMB.Lv;
            // 	long CategoryId = sphereMB.CategoryId;
            // 	sphereMB2 = SphereTable.GetByCategoryIdAndLevel(CategoryId, Lv);
            // 	if (sphereMB2 == 0)
            // 	{
            // 		goto IL_E3;
            // 	}
            // 	IReadOnlyList<UserItem> ItemListRequiredToCombine = sphereMB2.ItemListRequiredToCombine;
            // 	num = 0;
            // 	if (sphereMB2 != 0)
            // 	{
            // 		uint num2;
            // 		if (num >= (int)num2)
            // 		{
            // 			goto IL_4C;
            // 		}
            // 		num += num;
            // 		if (num != (int)num2)
            // 		{
            // 			num++;
            // 			goto IL_4C;
            // 		}
            // 		goto IL_52;
            // 		IL_56:
            // 		while (sphereMB2 != (ulong)3L)
            // 		{
            // 		}
            // 		goto IL_5C;
            // 		IL_52:
            // 		sphereMB2 += sphereMB2;
            // 		goto IL_56;
            // 		IL_4C:
            // 		if (sphereMB2 == (ulong)1L)
            // 		{
            // 			goto IL_52;
            // 		}
            // 		goto IL_56;
            // 	}
            // 	IL_5C:
            // 	if ("{il2cpp array field local12->}" != (ulong)0L)
            // 	{
            // 	}
            // }
            // while (num != 0);
            // UserDataManager instance = SingletonMonoBehaviour.Instance;
            // long Id = sphereMB2.Id;
            // long userItemCount = instance.GetUserItemCount(ItemType.Sphere, Id);
            // UserItem userItem = new UserItem();
            // userItem.ItemType = (ItemType)((ulong)14L);
            // long <Id>k__BackingField2 = sphereMB2.Id;
            // userItem.ItemId = <Id>k__BackingField2;
            // userItem.ItemCount = count;
            // bool flag;
            // return flag;
            // IL_E3:
            // throw new NullReferenceException();
            throw new NotImplementedException();
        }

        private static bool IsUseFixJst(ItemType itemType)
        {
            return itemType == ItemType.GachaTicket;
        }
    }
}