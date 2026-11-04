using System.ComponentModel;

[Description("決済手段タイプ")]
public enum ShopPayType
{
    [Description("クレジット")]
    CreditCard,
    [Description("au PAY（auかんたん決済）")]
    AuPay = 8,
    [Description("d払い")]
    DocomoPay,
    [Description("ソフトバンクまとめて支払い（B）")]
    SoftBankPay = 11,
    [Description("ApplePay")]
    ApplePay = 27,
    [Description("AmazonPay")]
    AmazonPay = 38,
    [Description("PayPay")]
    PayPay = 45,
    [Description("au PAY（ネット支払い）ウェブ方式")]
    AuPayNet = 49,
    [Description("GooglePay(Gmoにはないタイプ)")]
    GooglePay = 200,
    [Description("クレジットカード(Stripe)")]
    StripeCreditCard = 300
}
