using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
    [MessagePackObject(true)]
    public class BattleRewardResult
    {
        public long CharacterExp { get; set; }

        public List<UserItem> DropItemList { get; set; }

        public long ExtraGold { get; set; }

        public List<UserItem> FixedItemList { get; set; }

        public long PlayerExp { get; set; }

        public long RankUp { get; set; }

        public List<IUserItem> GetRewards(bool isDropItem = true)
        {
            List<IUserItem> list = new List<IUserItem>();
            UserItem userItem = new UserItem();
            userItem.ItemType = (ItemType) ((ulong) 22L);
            userItem.ItemId = (long) ((ulong) 1L);
            long num = this.PlayerExp;
            userItem.ItemCount = num;
            list.Add(userItem);
            UserItem userItem2 = new UserItem();
            userItem2.ItemType = (ItemType) ((ulong) 3L);
            userItem2.ItemId = (long) ((ulong) 1L);
            long num2 = this.ExtraGold;
            userItem2.ItemCount = num2;
            list.Add(userItem2);
            UserItem userItem3 = new UserItem();
            userItem3.ItemType = (ItemType) ((ulong) 11L);
            userItem3.ItemId = (long) ((ulong) 1L);
            long num3 = this.CharacterExp;
            userItem3.ItemCount = num3;
            list.Add(userItem3);
            if (isDropItem && !this.DropItemList.IsNullOrEmpty<UserItem>())
            {
                List<UserItem> list2 = this.DropItemList;
                list.AddRange(list2);
            }

            if (!this.FixedItemList.IsNullOrEmpty<UserItem>())
            {
                List<UserItem> list3 = this.FixedItemList;
                list.AddRange(list3);
            }

            return UserItemProvider.MergeSameItem(list);
        }

        public BattleRewardResult()
        {
        }
    }
}