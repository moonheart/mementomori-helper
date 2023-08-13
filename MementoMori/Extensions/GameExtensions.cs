using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.DtoInfo;
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
}