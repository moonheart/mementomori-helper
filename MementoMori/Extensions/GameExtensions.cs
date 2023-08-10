using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.Extensions;

public static class GameExtensions
{
    public static void PrintUserItems(this IEnumerable<IUserItem> userItems, Action<string> log)
    {
        if (userItems == null)
        {
            return ;
        }

        foreach (var userItem in userItems)
        {
            var itemName = ItemUtil.GetItemName(userItem.ItemType, userItem.ItemId);
            var itemRarity = ItemUtil.GetItemRarity(userItem.ItemType, userItem.ItemId);
            log($"名称：{itemName}, 稀有度: {itemRarity}, 数量： {userItem.ItemCount}");
        }
    }
}