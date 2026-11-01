using System.ComponentModel;

[Description("ストーリーオブジェクトタイプ")]
public enum StoryObjectType
{
    [Description("なし")]
    None = -1,
    [Description("デフォルト")]
    Default,
    [Description("変化1")]
    Change1,
    [Description("変化2")]
    Change2,
    [Description("選択3")]
    Select1,
    [Description("選択4")]
    Select2,
    [Description("選択5")]
    Select3
}
