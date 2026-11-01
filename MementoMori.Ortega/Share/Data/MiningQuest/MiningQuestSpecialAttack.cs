using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestSpecialAttack
{
    [Description("特殊攻撃のId")]
    public long SpecialAttackId { get; set; }

    [Description("特殊攻撃タイプ")]
    public MiningQuestSpecialAttackType Type { get; set; }

    [Description("初回発動クールタイム")]
    public int InitialCoolTimeMilliseconds { get; set; }

    [Description("基礎クールタイム")]
    public int BaseCoolTimeMilliseconds { get; set; }

    [Description("攻撃力参照倍率")]
    public int AttackPowerRate { get; set; }

    [Nest(true, 1)]
    [Description("攻撃範囲リスト")]
    public IReadOnlyList<MiningQuestSpecialAttackRange> AttackRangeList { get; set; }

    [Description("再生速度")]
    public int RangeExpansionIntervalMilliseconds { get; set; }

    [Description("基礎同時発生数")]
    public int BaseSimultaneousCount { get; set; }
}
