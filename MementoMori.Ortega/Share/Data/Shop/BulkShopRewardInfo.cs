using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Data.Item.Model;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop;

[MessagePackObject(true)]
public class BulkShopRewardInfo
{
    public List<UserItem> BonusItemList { get; set; }

    public List<UserCharacter> CharacterList { get; set; }

    public List<UserItem> ItemList { get; set; }

    public ShopProductType ShopProductType { get; set; }

    public int Count { get; set; }

    public int Price { get; set; }

    public string ShopProductNameKey { get; set; }
}
