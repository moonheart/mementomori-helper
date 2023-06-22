using System.Text;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori.WebUI.Extensions;

public static class GameExtensions
{
    public static void PrintUserItems(this IEnumerable<IUserItem> userItems, StringBuilder stringBuilder)
    {
        if (userItems == null)
        {
            return ;
        }

        foreach (var userItem in userItems)
        {
            var itemMb = Masters.ItemTable.GetByItemTypeAndItemId(userItem.ItemType, userItem.ItemId);
            if (itemMb == null)
            {
                stringBuilder.AppendLine($"ID：{userItem.ItemId}, 类型: {userItem.ItemType}, 数量： {userItem.ItemCount}");
            }
            else
            {
                var name = Masters.TextResourceTable.Get(itemMb.NameKey);
                var displayName = Masters.TextResourceTable.Get(itemMb.DisplayName);
                stringBuilder.AppendLine($"名称：{displayName}, 稀有度: {itemMb.ItemRarityFlags}, 数量： {userItem.ItemCount}");
            }
        }
    }
}