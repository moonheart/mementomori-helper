using System.ComponentModel;

[Description("Shop決済状態タイプ")]
public enum ShopPaymentStateType
{
    [Description("未決済")]
    None,
    [Description("決済成功")]
    Purchase,
    [Description("決済失敗")]
    InvalidPoint,
    [Description("返金")]
    Return,
    [Description("キャンセル")]
    Cancel,
    [Description("セッション切れ(前回セッションが残っていてセッションを強制的に切ったときのステータス)")]
    ExpiredSession,
    [Description("補填")]
    Present,
    [Description("回収")]
    Retrieve
}
