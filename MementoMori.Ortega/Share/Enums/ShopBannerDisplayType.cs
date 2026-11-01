using System.ComponentModel;

[Description("ショップバナーの表示タイプ")]
public enum ShopBannerDisplayType
{
    [Description("なし")]
    None,
    [Description("バナー上にテキストと公式サイトボタンを表示する")]
    Type1,
    [Description("バナー上にテキストのみ表示する")]
    Type2,
    [Description("バナー上にテキストと検索窓UIを表示する")]
    Type3
}
