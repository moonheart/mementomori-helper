using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums;

[Description("パネル図鑑の短冊の表示形式")]
public enum PanelPictureBookDisplayFormatType
{
    [Description("見出し")] Headline,
    [Description("PanelMBに登録されている動画")] MoviePanelMB,
    [Description("オープニング動画")] MovieOpening
}