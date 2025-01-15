using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data;

[MessagePackObject(true)]
[Description("アイテム所持上限クエスト切替")]
public class ItemMaxCountSwitchingQuestMB : MasterBookBase
{
    [PropertyOrder(2)]
    [Description("アイテムID")]
    public long ItemId { get; }

    [PropertyOrder(1)]
    [Description("アイテム種別")]
    public ItemType ItemType { get; }

    [Nest(false, 0)]
    [PropertyOrder(3)]
    [Description("所持数上限切替クエストリスト")]
    public IReadOnlyList<MaxCountSwitchingQuest> MaxCountSwitchingQuestList { get; }

    [SerializationConstructor]
    public ItemMaxCountSwitchingQuestMB(long id, bool? isIgnore, string memo, long itemId, ItemType itemType, IReadOnlyList<MaxCountSwitchingQuest> maxCountSwitchingQuestList)
        : base(id, isIgnore, memo)
    {
        ItemId = itemId;
        ItemType = itemType;
        MaxCountSwitchingQuestList = maxCountSwitchingQuestList;
    }

    public ItemMaxCountSwitchingQuestMB()
        : base(0L, null, null)
    {
    }

    public MaxCountSwitchingQuest GetMaxCountSwitchingQuest(long questId)
    {
        return null;
    }
}