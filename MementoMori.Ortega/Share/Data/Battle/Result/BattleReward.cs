using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Extensions;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class BattleReward
    {
        public int CharacterExp { get; set; }

        public List<UserItem> DropItemList { get; set; }

        public List<UserItem> FixedItemList { get; set; }

        public int PlayerExp { get; set; }

        public long PopulationGold { get; set; }

        public long PopulationPotentialJewel { get; set; }

        public void Add(BattleReward other)
        {
            int num = this.PlayerExp;
            List<UserItem> list = this.DropItemList;
            this.PlayerExp = num;
            int num2 = this.CharacterExp;
            this.CharacterExp = num2;
            List<UserItem> list2 = other.DropItemList;
            list.AddRange(list2);
            List<UserItem> list3 = this.FixedItemList;
            List<UserItem> list4 = other.FixedItemList;
            list3.AddRange(list4);
        }

        // public static BattleReward CreateFromFixedItem(List<IUserItem> fixedItem)
        // {
        // 	BattleReward battleReward = new BattleReward();
        // 	List<UserItem> list = new List<UserItem>();
        // 	battleReward.DropItemList = list;
        // 	List<UserItem> list2 = new List<UserItem>();
        // 	battleReward.FixedItemList = list2;
        // 	Func<IUserItem, UserItem> <>9__25_ = BattleReward.<>c.<>9__25_0;
        // 	if (<>9__25_ == 0)
        // 	{
        // 		Func<IUserItem, UserItem> func;
        // 		BattleReward.<>c.<>9__25_0 = func;
        // 	}
        // 	List<UserItem> list3 = Enumerable.ToList<UserItem>(Enumerable.Select<IUserItem, UserItem>(fixedItem, <>9__25_));
        // 	battleReward.FixedItemList = list3;
        // 	return battleReward;
        // }
        
        public List<IUserItem> GetRewards()
        {
            List<IUserItem> list = new();
            if (!this.DropItemList.IsNullOrEmpty<UserItem>())
            {
                List<UserItem> list2 = this.DropItemList;
                list.AddRange(list2);
            }
            if (!this.FixedItemList.IsNullOrEmpty<UserItem>())
            {
                List<UserItem> list3 = this.FixedItemList;
                list.AddRange(list3);
            }
            List<IUserItem> list4 = UserItemProvider.MergeSameItem(list);
            
            list4.Sort((a, b) => a.ItemId > b.ItemId ? 1 : -1);
            return list4;
        }

        public BattleReward()
        {
            this.DropItemList = new List<UserItem>();
            this.FixedItemList = new List<UserItem>();
        }
    }
}