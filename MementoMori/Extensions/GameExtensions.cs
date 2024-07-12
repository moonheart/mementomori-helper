using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Extensions;

namespace MementoMori.Extensions;

public static class GameExtensions
{
    public static void PrintUserItems(this IEnumerable<IUserItem> userItems, Action<string> log)
    {
        if (userItems == null) return;

        foreach (var userItem in userItems)
        {
            var itemName = ItemUtil.GetItemName(userItem.ItemType, userItem.ItemId);
            var itemRarity = ItemUtil.GetItemRarity(userItem.ItemType, userItem.ItemId);
            log($"{ResourceStrings.Name}: {itemName}({itemRarity}) × {userItem.ItemCount}");
        }
    }

    public static void PrintUserItems(this IEnumerable<IUserCharacterItem> userItems, Action<string> log)
    {
        if (userItems == null) return;

        foreach (var userItem in userItems)
        {
            var itemName = ItemUtil.GetItemName(userItem.Item.ItemType, userItem.Item.ItemId);
            var itemRarity = ItemUtil.GetItemRarity(userItem.Item.ItemType, userItem.Item.ItemId);
            log($"{ResourceStrings.Name}: {itemName}({itemRarity}) × {userItem.Item.ItemCount}");
        }
    }

    public static void PrintCharacterDtos(this IEnumerable<UserCharacterDtoInfo> userItems, Action<string> log)
    {
        if (userItems == null) return;

        foreach (var userItem in userItems)
        {
            var characterMb = CharacterTable.GetById(userItem.CharacterId);
            var name = TextResourceTable.Get(characterMb.NameKey);
            log($"{ResourceStrings.Name}: {name}({characterMb.RarityFlags}) Lv{userItem.Level}");
        }
    }

    public static long GetCount(this IEnumerable<IUserItem> userItems, ItemType itemType, long itemId = 1)
    {
        var items = userItems.ToList();
        if (items.IsNullOrEmpty()) return 0;
        return items.FirstOrDefault(d => d.ItemType == itemType && d.ItemId == itemId)?.ItemCount ?? 0;
    }
}