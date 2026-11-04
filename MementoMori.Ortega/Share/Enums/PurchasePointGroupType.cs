using System.ComponentModel;

[Description("ポイント取得タイプ")]
public enum PurchasePointGroupType
{
    [Description("Stripe")]
    Stripe,
    [Description("GMO")]
    Gmo,
    [Description("管理画面")]
    Admin = 100
}
