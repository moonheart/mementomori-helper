using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gacha;

[MessagePackObject(true)]
public class GachaResultItem : IUserItem
{
    public CharacterRarityFlags CharacterRarityFlags { get; set; }

    public long GachaLotteryId { get; set; }

    public long GachaLotteryItemListId { get; set; }

    public string Guid { get; set; }

    public bool IsCeilingItem { get; set; }

    public long ItemCount { get; set; }

    public long ItemId { get; set; }

    public ItemType ItemType { get; set; }

    public IUserItem GetUserItem()
    {
        if (ItemType != ItemType.Character)
        {
            var userItem = new UserItem();
            var itemType = ItemType;
            userItem.ItemType = itemType;
            var num = ItemId;
            userItem.ItemId = num;
            var num2 = ItemCount;
            userItem.ItemCount = num2;
        }

        var characterRarityFlags = CharacterRarityFlags;
        throw new NullReferenceException();
    }
}