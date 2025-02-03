using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TradeShop;

[MessagePackObject(true)]
public class TradeShopItem
{
    [Description("TradeShopItemMBのId")]
    public long TradeShopItemId { get; set; }

    [Description("消費アイテム1")]
    public UserItem ConsumeItem1 { get; set; }

    [Description("消費アイテム2")]
    public UserItem ConsumeItem2 { get; set; }

    [Description("獲得アイテム")]
    public UserItem GiveItem { get; set; }

    [Description("割引率")]
    public int SalePercent { get; set; }

    [Description("交換回数")]
    public int TradeCount { get; set; }

    [Description("交換制限回数(0:無制限s)")]
    public int LimitTradeCount { get; set; }

    [Description("神器タイプ")]
    public SacredTreasureType SacredTreasureType { get; set; }

    [Description("並び順")]
    public int SortOrder { get; set; }

    [Description("条件キャラID")]
    public long RequiredCharacterId { get; set; }

    [Description("購入不可フラグ")]
    public bool Disabled { get; set; }

    [Description("終了日時")]
    public long ExpirationTimeStamp { get; set; }

    [Description("TradeShopDedicatedItemMBのID")]
    public long DedicatedItemId { get; set; }

    [IgnoreMember]
    public bool IsDedicated
    {
        get
        {
            return false;
        }
    }

    public bool IsSoldOut()
    {
        return LimitTradeCount > 0 && TradeCount >= LimitTradeCount;
    }

    public bool IsFree()
    {
        return ConsumeItem1 == null && ConsumeItem2 == null;
    }
}