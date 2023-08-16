using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class AutoBattleRewardResult
    {
        public int BattleCountAll { get; set; }

        public int BattleCountWin { get; set; }

        public BattleRewardResult BattleRewardResult { get; set; }

        public long BattleTotalTime { get; set; }

        public long GoldByPopulation { get; set; }

        public long PotentialJewelByPopulation { get; set; }

        public List<IUserItem> GetTotalRewards()
        {
            List<IUserItem> list = new List<IUserItem>();
            List<IUserItem> list2 = new List<IUserItem>();
            UserItem userItem = new UserItem();
            userItem.ItemType = ItemType.Gold;
            userItem.ItemId = 1;
            userItem.ItemCount = GoldByPopulation;
            list2.Add(userItem);
            UserItem userItem2 = new UserItem();
            userItem2.ItemType = ItemType.CharacterTrainingMaterial;
            userItem2.ItemId = 2;
            userItem2.ItemCount = PotentialJewelByPopulation;
            list2.Add(userItem2);
            list.AddRange(list2);
            List<IUserItem> rewards = this.BattleRewardResult.GetRewards(true);
            list.AddRange(rewards);
            return UserItemProvider.MergeSameItem(list);
        }

        public List<IUserItem> GetPopulationRewards()
        {
            List<IUserItem> list = new List<IUserItem>();
            UserItem userItem = new UserItem();
            userItem.ItemType = (ItemType) ((ulong) 3L);
            userItem.ItemId = (long) ((ulong) 1L);
            userItem.ItemCount = GoldByPopulation;
            list.Add(userItem);
            UserItem userItem2 = new UserItem();
            userItem2.ItemType = (ItemType) ((ulong) 11L);
            userItem2.ItemId = (long) ((ulong) 2L);
            long num2 = this.PotentialJewelByPopulation;
            userItem2.ItemCount = num2;
            list.Add(userItem2);
            return list;
        }

        public List<IUserItem> GetRewards()
        {
            return this.BattleRewardResult.GetRewards(true);
        }

        public bool IsExistReward()
        {
            return !BattleRewardResult.DropItemList.IsNullOrEmpty();
        }

        public AutoBattleRewardResult()
        {
        }
    }
}