using System.ComponentModel;

[Description("ストーリーマップの演出コマンドタイプ")]
public enum StoryMapCommandType
{
    [Description("なし")]
    None,
    [Description("ストーリーに移動")]
    MoveToStory,
    [Description("呪い解除")]
    CureToCurse,
    [Description("次の国に移動")]
    MoveToNextState,
    [Description("オブジェクト生成")]
    CreateObject = 11,
    [Description("オブジェクト削除")]
    RemoveObject,
    [Description("オブジェクト変化1")]
    ChangeToObject1,
    [Description("オブジェクト変化2")]
    ChangeToObject2,
    [Description("オブジェクト選択生成")]
    SelectNothingToObject = 21,
    [Description("オブジェクト選択変化")]
    SelectObjectToObject
}
