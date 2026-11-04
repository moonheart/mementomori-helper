using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data;

[MessagePackObject(true)]
public class CustomTextSimpleLayout
{
    [Nest(true, 2)]
    [PropertyOrder(1)]
    [Description("日本語")]
    public CustomTextSimpleLayoutInfo jaJP { get; set; }

    [Nest(true, 2)]
    [PropertyOrder(2)]
    [Description("英語")]
    public CustomTextSimpleLayoutInfo enUS { get; set; }

    [Nest(true, 2)]
    [PropertyOrder(3)]
    [Description("韓国語")]
    public CustomTextSimpleLayoutInfo koKR { get; set; }

    [Nest(true, 2)]
    [PropertyOrder(4)]
    [Description("中国語(繁体字)")]
    public CustomTextSimpleLayoutInfo zhTW { get; set; }

    [Nest(true, 2)]
    [PropertyOrder(5)]
    [Description("中国語(簡体字)")]
    public CustomTextSimpleLayoutInfo zhCN { get; set; }
}
