using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestReinforcementInfo
{
    [Description("強化レベル")]
    public int Level { get; set; }

    [Description("必要通貨アイテム数量")]
    public int RequiredItemCount { get; set; }

    [Description("解放条件回収Lv")]
    public int RequiredCollectionLevel { get; set; }

    [Nest(true, 1)]
    [Description("該当Lvで適用されるパラメータリスト")]
    public IReadOnlyList<MiningQuestReinforcementParameter> ParameterList { get; set; }
}
