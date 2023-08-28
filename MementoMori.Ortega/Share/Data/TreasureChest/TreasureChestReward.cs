using System.Data;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TreasureChest
{
	[MessagePackObject(true)]
	public class TreasureChestReward : IUserCharacterItem
	{
		public SacredTreasureType SacredTreasureType { get; set; }

		public CharacterRarityFlags RarityFlags { get; set; }

		public UserItem Item { get; set; }

		public static List<TreasureChestReward> SumSameItem(IEnumerable<TreasureChestReward> sourceItems)
		{
			// List<TreasureChestReward> list;
			// for (;;)
			// {
			// 	list = new List();
			// 	int num = 0;
			// 	uint num2;
			// 	if (num >= (int)num2)
			// 	{
			// 		goto IL_1E;
			// 	}
			// 	num += num;
			// 	if (num != (int)num2)
			// 	{
			// 		num++;
			// 		goto IL_1E;
			// 	}
			// 	IL_23:
			// 	if (num != 0)
			// 	{
			// 		goto IL_85;
			// 	}
			// 	TreasureChestReward treasureChestReward;
			// 	UserItem userItem;
			// 	treasureChestReward.<Item>k__BackingField = userItem;
			// 	treasureChestReward.<RarityFlags>k__BackingField = userItem;
			// 	treasureChestReward.<SacredTreasureType>k__BackingField = userItem;
			// 	list.Add(treasureChestReward);
			// 	if ("{il2cpp array field local6->}" != (ulong)0L)
			// 	{
			// 	}
			// 	if ("{il2cpp array field local6->}" != (ulong)0L)
			// 	{
			// 	}
			// 	if (num == 0)
			// 	{
			// 		break;
			// 	}
			// 	continue;
			// 	IL_1E:
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		goto IL_23;
			// 	}
			// 	goto IL_23;
			// }
			// return list;
			// IL_85:
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static TreasureChestReward ConvertToTreasureChestReward(IUserItem item)
		{
			// TreasureChestReward treasureChestReward = new TreasureChestReward();
			// UserItem userItem = item.ToUserItem();
			// treasureChestReward.<Item>k__BackingField = userItem;
			// int num = 0;
			// treasureChestReward.<RarityFlags>k__BackingField = userItem;
			// treasureChestReward.<SacredTreasureType>k__BackingField = (SacredTreasureType)num;
			// return treasureChestReward;
			throw new RowNotInTableException();
		}

		public bool IsSameItem(TreasureChestReward reward)
		{
			// UserItem userItem = reward.<Item>k__BackingField;
			// ItemType <ItemType>k__BackingField = this.<Item>k__BackingField.<ItemType>k__BackingField;
			// if (userItem.<ItemType>k__BackingField == <ItemType>k__BackingField)
			// {
			// 	long <ItemId>k__BackingField = this.<Item>k__BackingField.<ItemId>k__BackingField;
			// 	if (userItem.<ItemId>k__BackingField == <ItemId>k__BackingField)
			// 	{
			// 		CharacterRarityFlags characterRarityFlags = this.<RarityFlags>k__BackingField;
			// 		if (reward.<RarityFlags>k__BackingField == characterRarityFlags)
			// 		{
			// 			SacredTreasureType sacredTreasureType = this.<SacredTreasureType>k__BackingField;
			// 			return reward.<SacredTreasureType>k__BackingField == sacredTreasureType;
			// 		}
			// 	}
			// }
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}
	}
}
