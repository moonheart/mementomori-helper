using System.ComponentModel;

namespace MementoMori.Ortega.Share.Data.Equipment
{
    public interface IReadOnlyEquipment
    {
        [Description("付与パラメータ(技力)")]
        long AdditionalParameterEnergy { get; }

        [Description("付与パラメータ(体力)")]
        long AdditionalParameterHealth { get; }

        [Description("付与パラメータ(知力)")]
        long AdditionalParameterIntelligence { get; }

        [Description("付与パラメータ(筋力)")]
        long AdditionalParameterMuscle { get; }

        [Description("武具ID")]
        long EquipmentId { get; }

        [Description("固有ID")]
        string Guid { get; }

        [Description("聖装経験値")]
        long LegendSacredTreasureExp { get; }

        [Description("聖装レベル")]
        long LegendSacredTreasureLv { get; }

        [Description("魔装経験値")]
        long MatchlessSacredTreasureExp { get; }

        [Description("魔装レベル")]
        long MatchlessSacredTreasureLv { get; }

        [Description("強化レベル")]
        long ReinforcementLv { get; }

        [Description("宝石ID1")]
        long SphereId1 { get; }

        [Description("宝石ID2")]
        long SphereId2 { get; }

        [Description("宝石ID3")]
        long SphereId3 { get; }

        [Description("宝石ID4")]
        long SphereId4 { get; }

        [Description("宝石スロット解放数")]
        long SphereUnlockedCount { get; }

        [Description("シンクロ反映前の武具ID")]
        long BeforeSynchroEquipmentId { get; set; }

        [Description("ベース枠に設定中のシンクログループID")]
        long SetBaseSynchroGroupId { get; set; }

        [Description("プレイヤーID")]
        long PlayerId { get; set; }
    }
}