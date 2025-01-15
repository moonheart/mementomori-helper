using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item;

[MessagePackObject(true)]
[Description("アイテム所持上限確認済みクエスト")]
public class ConfirmedItemQuest
{
    [PropertyOrder(2)]
    [Description("アイテムId")]
    public long ItemId { get; set; }

    [PropertyOrder(1)]
    [Description("アイテムタイプ")]
    public ItemType ItemType { get; set; }

    [PropertyOrder(3)]
    [Description("クエストID")]
    public long QuestId { get; set; }
}