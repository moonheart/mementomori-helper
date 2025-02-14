using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

[Description("交換所種類")]
public enum TradeShopType
{
    [Description("一般")] 
    Normal,
    [Description("通常武具合成")]
    NormalEquipmentComposite,
    [Description("パターン2")]
    Fix,
    [Description("店舗")]
    Store,
    [Description("スフィア")] 
    Sphere,
    [Description("週間トピックス無料商品")]
    WeeklyTopicsFreeProduct = 6,
    [Description("週間トピックス有料商品")]
    WeeklyTopicsPaidProduct,
    [Description("専用武器")]
    ExclusiveEquipment
}