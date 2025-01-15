using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item;

[MessagePackObject(true)]
[Description("所持数上限切替クエスト")]
public class MaxCountSwitchingQuest
{
    [PropertyOrder(1)]
    [Description("クエストID")]
    public long QuestId { get; set; }

    [PropertyOrder(2)]
    [Description("所持数上限")]
    public long MaxCount { get; set; }
}