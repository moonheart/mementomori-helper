using MementoMori.Ortega.Share.Data.Item.Model;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item
{
	public static class UserItemProvider
	{
		// public static IUserItem GetUserItem(ItemType itemType, long itemId, long itemCount, CharacterRarityFlags characterRarityFlags = CharacterRarityFlags.None)
		// {
		// 	if (itemType - ItemType.CurrencyFree <= 49)
		// 	{
		// 		UserCurrencyFree userCurrencyFree;
		// 		userCurrencyFree.ItemType = (ItemType)((ulong)1L);
		// 		userCurrencyFree.ItemId = (long)((ulong)1L);
		// 		userCurrencyFree.ItemCount = itemCount;
		// 		Type typeFromHandle = typeof(DeviceType);
		// 		int num = 0;
		// 		object obj2;
		// 		object obj = obj2;
		// 		UserCurrencyPaid userCurrencyPaid;
		// 		userCurrencyPaid.ItemType = (ItemType)((ulong)2L);
		// 		int num2 = 0;
		// 		userCurrencyPaid.FieldGetter(num2, num, characterRarityFlags);
		// 		userCurrencyPaid.ItemId = obj;
		// 		userCurrencyPaid.ItemCount = itemCount;
		// 		UserGold userGold;
		// 		userGold.ItemType = (ItemType)((ulong)3L);
		// 		UserEquipmentFragment userEquipmentFragment;
		// 		userEquipmentFragment.ItemType = (ItemType)((ulong)5L);
		// 		userEquipmentFragment.ItemId = userCurrencyPaid;
		// 		UserCharacterFragment userCharacterFragment;
		// 		userCharacterFragment.ItemType = (ItemType)((ulong)7L);
		// 		userCharacterFragment.ItemId = userCurrencyPaid;
		// 		UserEquipmentSetMaterial userEquipmentSetMaterial;
		// 		userEquipmentSetMaterial.ItemType = (ItemType)((ulong)9L);
		// 		userEquipmentSetMaterial.ItemId = userCurrencyPaid;
		// 		UserQuestQuickTicket userQuestQuickTicket;
		// 		userQuestQuickTicket.ItemType = (ItemType)((ulong)10L);
		// 		userQuestQuickTicket.ItemId = userCurrencyPaid;
		// 		UserCharacterTrainingMaterial userCharacterTrainingMaterial;
		// 		userCharacterTrainingMaterial.ItemType = (ItemType)((ulong)11L);
		// 		Type typeFromHandle2 = typeof(EquipmentReinforcementItemType);
		// 		UserEquipmentReinforcementItem userEquipmentReinforcementItem;
		// 		userEquipmentReinforcementItem.ItemType = (ItemType)((ulong)12L);
		// 		Type typeFromHandle3 = typeof(ExchangePlaceItemType);
		// 		UserExchangePlaceItem userExchangePlaceItem;
		// 		userExchangePlaceItem.ItemType = (ItemType)((ulong)13L);
		// 		UserSphere userSphere;
		// 		userSphere.ItemType = (ItemType)((ulong)14L);
		// 		userSphere.ItemCount = itemCount;
		// 		userSphere.ItemId = userExchangePlaceItem;
		// 		Type typeFromHandle4 = typeof(MatchlessSacredTreasureExpItemType);
		// 		UserMatchlessSacredTreasureExpItem userMatchlessSacredTreasureExpItem;
		// 		userMatchlessSacredTreasureExpItem.ItemType = (ItemType)((ulong)15L);
		// 		UserGachaTicket userGachaTicket;
		// 		userGachaTicket.ItemType = (ItemType)((ulong)16L);
		// 		userGachaTicket.ItemId = userMatchlessSacredTreasureExpItem;
		// 		UserTreasureChest userTreasureChest;
		// 		userTreasureChest.ItemType = (ItemType)((ulong)17L);
		// 		userTreasureChest.ItemId = userMatchlessSacredTreasureExpItem;
		// 		UserTreasureChestKey userTreasureChestKey;
		// 		userTreasureChestKey.ItemType = (ItemType)((ulong)18L);
		// 		userTreasureChestKey.ItemId = userMatchlessSacredTreasureExpItem;
		// 		UserBossChallengeTicket userBossChallengeTicket;
		// 		userBossChallengeTicket.ItemType = (ItemType)((ulong)19L);
		// 		UserTowerBattleTicket userTowerBattleTicket;
		// 		userTowerBattleTicket.ItemType = (ItemType)((ulong)20L);
		// 		UserDungeonRecoveryItem userDungeonRecoveryItem;
		// 		userDungeonRecoveryItem.ItemType = (ItemType)((ulong)21L);
		// 		UserFriendPoint userFriendPoint;
		// 		userFriendPoint.ItemType = (ItemType)((ulong)23L);
		// 		UserEquipmentRarityCrystal userEquipmentRarityCrystal;
		// 		userEquipmentRarityCrystal.ItemType = (ItemType)((ulong)24L);
		// 		UserLevelLinkExp userLevelLinkExp;
		// 		userLevelLinkExp.ItemType = (ItemType)((ulong)25L);
		// 		UserGuildFame userGuildFame;
		// 		userGuildFame.ItemType = (ItemType)((ulong)26L);
		// 		UserGuildExp userGuildExp;
		// 		userGuildExp.ItemType = (ItemType)((ulong)27L);
		// 		Type typeFromHandle5 = typeof(ActivityMedalType);
		// 		UserActivityMedal userActivityMedal;
		// 		userActivityMedal.ItemType = (ItemType)((ulong)28L);
			// UserVipExp userVipExp;
			// userVipExp.<ItemType>k__BackingField = (ItemType)((ulong)29L);
		// 		UserEventExchangePlaceItem userEventExchangePlaceItem;
		// 		userEventExchangePlaceItem.ItemType = (ItemType)((ulong)50L);
		// 		userEventExchangePlaceItem.ItemId = userActivityMedal;
		// 	}
		// 	string text;
		// 	Exception ex = new Exception(text);
		// 	throw new NullReferenceException();
		// }

		// public static List<UserItem> SumSameItem(IEnumerable<IUserItem> sourceItems)
		// {
		// 	List<UserItem> list;
		// 	ulong num2;
		// 	do
		// 	{
		// 		list = new List();
		// 		bool flag;
		// 		if (flag)
		// 		{
		// 		}
		// 		ulong num;
		// 		if (num != (ulong)0L)
		// 		{
		// 			goto IL_52;
		// 		}
		// 		UserItem userItem;
		// 		list.Add(userItem);
		// 		if ("{il2cpp array field local6->}" != (ulong)0L)
		// 		{
		// 		}
		// 		if ("{il2cpp array field local6->}" != (ulong)0L)
		// 		{
		// 		}
		// 	}
		// 	while (num2 != (ulong)0L);
		// 	return list;
		// 	IL_52:
		// 	throw new NullReferenceException();
		// }

		// public static List<UserItem> SumSameItem(IEnumerable<UserItem> sourceItems)
		// {
		// 	List<UserItem> list;
		// 	ulong num2;
		// 	do
		// 	{
		// 		list = new List();
		// 		bool flag;
		// 		if (flag)
		// 		{
		// 		}
		// 		ulong num;
		// 		if (num != (ulong)0L)
		// 		{
		// 			goto IL_4C;
		// 		}
		// 		UserItem userItem;
		// 		list.Add(userItem);
		// 		if ("{il2cpp array field local6->}" != (ulong)0L)
		// 		{
		// 		}
		// 		if ("{il2cpp array field local6->}" != (ulong)0L)
		// 		{
		// 		}
		// 	}
		// 	while (num2 != (ulong)0L);
		// 	return list;
		// 	IL_4C:
		// 	throw new NullReferenceException();
		// }

		public static List<IUserItem> MergeSameItem(IEnumerable<IUserItem> itemList)
		{
			throw new NotImplementedException();
			return itemList.ToList();
			// int num = 0;
			// Dictionary<ItemType, Dictionary<long, long>> dictionary = new Dictionary();
			// List<IUserItem> list = new List();
			// if (list != 0)
			// {
			// 	object syncRoot = list._syncRoot;
			// 	bool flag;
			// 	SacredTreasureType sacredTreasureType;
			// 	if (flag && sacredTreasureType != SacredTreasureType.None)
			// 	{
			// 		IUserItem userItem = list[num];
			// 		if (userItem == 0)
			// 		{
			// 		}
			// 		object syncRoot2 = list._syncRoot;
			// 		bool flag2;
			// 		if (flag2 && userItem != 0)
			// 		{
			// 			UserEquipment userEquipment;
			// 			if (sacredTreasureType == SacredTreasureType.Legend)
			// 			{
			// 				userEquipment.MatchlessSacredTreasureLv = (long)((ulong)1L);
			// 			}
			// 			bool flag3 = sacredTreasureType == SacredTreasureType.Matchless;
			// 			userEquipment.MatchlessSacredTreasureLv = (flag3 ? 1L : 0L);
			// 			userEquipment.LegendSacredTreasureLv = (long)num;
			// 			list[num] = userEquipment;
			// 		}
			// 	}
			// 	bool flag4;
			// 	if (!flag4)
			// 	{
			// 		Dictionary<long, long> dictionary2 = new Dictionary();
			// 	}
			// }
			// if ("{il2cpp array field local8->}" != (ulong)0L)
			// {
			// }
			// if (num == 0)
			// {
			// 	throw new NullReferenceException();
			// }
			// throw new NullReferenceException();
		}

		public static UserItem ToUserItem(this IUserItem item)
		{
			var userItem = new UserItem
			{
				ItemType = item.ItemType,
				ItemId = item.ItemId,
				ItemCount = item.ItemCount
			};
			return userItem;
		}
		//
		// public static PresentItem ToPresentItem(this IUserItem item)
		// {
		// 	PresentItem presentItem = new PresentItem();
		// 	UserItem userItem = new UserItem();
		// 	userItem.ItemType = userItem;
		// 	userItem.ItemId = userItem;
		// 	userItem.ItemCount = userItem;
		// 	presentItem.Item = userItem;
		// 	presentItem.RarityFlags = userItem;
		// 	return presentItem;
		// }
		//
		// public static IUserItem ToIUserItem(this IUserCharacterItem item)
		// {
		// 	UserItem userItem;
		// 	if (userItem == 0)
		// 	{
		// 		CharacterTable CharacterTable = Masters.CharacterTable;
		// 		CharacterMB characterMB;
		// 		if (characterMB != 0)
		// 		{
		// 		}
		// 	}
		// 	throw new NullReferenceException();
		// }
	}
}
