using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;

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
    public static void PrintUserItems(this IEnumerable<IUserCharacterItem> userItems, Action<string> log)
    {
        if (userItems == null)
        {
            return ;
        }

        foreach (var userItem in userItems)
        {
            var itemName = ItemUtil.GetItemName(userItem.Item.ItemType, userItem.Item.ItemId);
            var itemRarity = ItemUtil.GetItemRarity(userItem.Item.ItemType, userItem.Item.ItemId);
            log($"名称：{itemName}, 稀有度: {itemRarity}, 数量： {userItem.Item.ItemCount}");
        }
    }
    public static void PrintCharacterDtos(this IEnumerable<UserCharacterDtoInfo> userItems, Action<string> log)
    {
        if (userItems == null)
        {
            return ;
        }

        foreach (var userItem in userItems)
        {
            var characterMb = Masters.CharacterTable.GetById(userItem.CharacterId);
            var name = Masters.TextResourceTable.Get(characterMb.NameKey);
            log($"名称：{name}, 稀有度: {characterMb.RarityFlags}, 等级： {userItem.Level}");
        }
    }

    public static long GetCount(this IEnumerable<IUserItem> userItems, ItemType itemType, long itemId = 1)
    {
        var items = userItems.ToList();
        if (items.IsNullOrEmpty())
        {
            return 0;
        }
        return items.FirstOrDefault(d => d.ItemType == itemType && d.ItemId == itemId)?.ItemCount ?? 0;
    }

}