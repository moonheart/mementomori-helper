using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data;

[Description("アルカナレベル")]
[MessagePackObject(true)]
public class CharacterCollectionLevelMB : MasterBookBase
{
    [SerializationConstructor]
    public CharacterCollectionLevelMB(long id, bool? isIgnore, string memo, IReadOnlyList<BaseParameterChangeInfo> baseParameterChangeInfos,
        IReadOnlyList<BattleParameterChangeInfo> battleParameterChangeInfos, int characterRarityBonus, CharacterRarityFlags characterRarityFlags, int collectionId, int collectionLevel,
        int maxLevelIncreaseValue)
        : base(id, isIgnore, memo)
    {
        BaseParameterChangeInfos = baseParameterChangeInfos;
        BattleParameterChangeInfos = battleParameterChangeInfos;
        CharacterRarityBonus = characterRarityBonus;
        CharacterRarityFlags = characterRarityFlags;
        CollectionId = collectionId;
        CollectionLevel = collectionLevel;
        MaxLevelIncreaseValue = maxLevelIncreaseValue;
    }

    public CharacterCollectionLevelMB() : base(0, false, "")
    {
    }

    [Description("開放後適用されるベースパラメータ")]
    [PropertyOrder(4)]
    [Nest]
    public IReadOnlyList<BaseParameterChangeInfo> BaseParameterChangeInfos { get; }

    [Description("開放後適用されるバトルパラメータ")]
    [PropertyOrder(5)]
    [Nest]
    public IReadOnlyList<BattleParameterChangeInfo> BattleParameterChangeInfos { get; }

    [Description("キャラクターレアリティ増加")]
    [PropertyOrder(6)]
    public int CharacterRarityBonus { get; }

    [PropertyOrder(3)]
    [Description("アルカナレベル解放条件")]
    public CharacterRarityFlags CharacterRarityFlags { get; }

    [PropertyOrder(1)]
    [Description("アルカナMBのID")]
    public int CollectionId { get; }

    [PropertyOrder(2)]
    [Description("アルカナレベル")]
    public int CollectionLevel { get; }

    [PropertyOrder(7)]
    [Description("レベル上限増加")]
    public int MaxLevelIncreaseValue { get; }
}