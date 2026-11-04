using MementoMori.Ortega.Share.Enums;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.MiningQuest;

[MessagePackObject(true)]
public class MiningQuestSpecialAttackInfo
{
    [Description("特殊攻撃のId")]
    public long SpecialAttackId { get; set; }

    [Description("特殊攻撃タイプ")]
    public MiningQuestSpecialAttackType SpecialAttackType { get; set; }

    [Description("初回発動クールタイム")]
    public int InitialCoolTimeMilliseconds { get; set; }

    [Description("クールタイム")]
    public int CoolTimeMilliseconds { get; set; }

    [Description("攻撃力")]
    public int AttackPower { get; set; }

    [Description("攻撃範囲リスト（有効な範囲値を、範囲段階昇順に返す）")]
    public List<int> AttackRangeList { get; set; }

    [Description("再生速度")]
    public int RangeExpansionIntervalMilliseconds { get; set; }

    [Description("同時発生数")]
    public int SimultaneousCount { get; set; }
}
