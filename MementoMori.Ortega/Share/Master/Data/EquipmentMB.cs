using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("武具")]
    public class EquipmentMB : MasterBookBase
    {
        [Description("付加パラメータ合計値")]
        [PropertyOrder(10)]
        public long AdditionalParameterTotal { get; }

        [Description("進化先武具ID")]
        [PropertyOrder(16)]
        public long AfterLevelEvolutionEquipmentId { get; }

        [Description("進化先レアリティ武具ID")]
        [PropertyOrder(17)]
        public long AfterRarityEvolutionEquipmentId { get; }

        [Description("基礎パラメータ")]
        [Nest(false, 0)]
        [PropertyOrder(9)]
        public BattleParameterChangeInfo BattleParameterChangeInfo { get; }

        [Description("武具カテゴリ")]
        [PropertyOrder(7)]
        public EquipmentCategory Category { get; }

        [PropertyOrder(19)]
        [Description("合成情報ID")]
        public long CompositeId { get; }

        [PropertyOrder(18)]
        [Description("武具進化テーブルID")]
        public long EquipmentEvolutionId { get; }

        [PropertyOrder(15)]
        [Description("専属スキル説明文ID")]
        public long EquipmentExclusiveSkillDescriptionId { get; }

        [PropertyOrder(21)]
        [Description("鋳造ID")]
        public long EquipmentForgeId { get; }

        [PropertyOrder(3)]
        [Description("武具レベル")]
        public long EquipmentLv { get; }

        [Description("武具強化素材テーブルID")]
        [PropertyOrder(12)]
        public long EquipmentReinforcementMaterialId { get; }

        [Description("武具セットID")]
        [PropertyOrder(13)]
        public long EquipmentSetId { get; }

        [Description("装備可能キャラタイプ")]
        [PropertyOrder(6)]
        public JobFlags EquippedJobFlags { get; }

        [PropertyOrder(14)]
        [Description("専属効果ID")]
        public long ExclusiveEffectId { get; }

        [Description("最初の宝石スロット解放に必要な銅貨")]
        [PropertyOrder(23)]
        public long GoldRequiredToOpeningFirstSphereSlot { get; }

        [PropertyOrder(22)]
        [Description("ロック無し鍛錬に必要な銅貨")]
        public long GoldRequiredToTraining { get; }

        [PropertyOrder(24)]
        [Description("アイコンID")]
        public long IconId { get; }

        [Description("名称キー")]
        [PropertyOrder(1)]
        public string NameKey { get; }

        [Description("装備評価")]
        [PropertyOrder(4)]
        public long PerformancePoint { get; }

        [PropertyOrder(8)]
        [Description("品質レベル")]
        public int QualityLv { get; }

        [PropertyOrder(2)]
        [Description("レアリティ")]
        public EquipmentRarityFlags RarityFlags { get; }

        [Description("武具の装備枠タイプ")]
        [PropertyOrder(5)]
        public EquipmentSlotType SlotType { get; }

        [SerializationConstructor]
        public EquipmentMB(long id, bool? isIgnore, string memo, long additionalParameterTotal,
            long afterLevelEvolutionEquipmentId, long afterRarityEvolutionEquipmentId,
            BattleParameterChangeInfo battleParameterChangeInfo, EquipmentCategory category, long compositeId,
            long equipmentEvolutionId, long equipmentExclusiveSkillDescriptionId, long equipmentLv,
            long equipmentReinforcementMaterialId, long equipmentSetId, JobFlags equippedJobFlags,
            long exclusiveEffectId, long goldRequiredToOpeningFirstSphereSlot, long goldRequiredToTraining, long iconId,
            string nameKey, long performancePoint, long equipmentForgeId, int qualityLv,
            EquipmentRarityFlags rarityFlags, EquipmentSlotType slotType)
            : base(id, isIgnore, memo)
        {
            this.AdditionalParameterTotal = additionalParameterTotal;
            this.AfterLevelEvolutionEquipmentId = afterLevelEvolutionEquipmentId;
            this.AfterRarityEvolutionEquipmentId = afterRarityEvolutionEquipmentId;
            this.BattleParameterChangeInfo = battleParameterChangeInfo;
            this.Category = category;
            this.CompositeId = compositeId;
            this.EquipmentEvolutionId = equipmentEvolutionId;
            this.EquipmentExclusiveSkillDescriptionId = equipmentExclusiveSkillDescriptionId;
            this.EquipmentLv = equipmentLv;
            this.EquipmentReinforcementMaterialId = equipmentReinforcementMaterialId;
            this.EquipmentSetId = equipmentSetId;
            this.EquippedJobFlags = equippedJobFlags;
            this.ExclusiveEffectId = exclusiveEffectId;
            this.GoldRequiredToOpeningFirstSphereSlot = goldRequiredToOpeningFirstSphereSlot;
            this.GoldRequiredToTraining = goldRequiredToTraining;
            this.IconId = iconId;
            this.NameKey = nameKey;
            this.PerformancePoint = performancePoint;
            this.EquipmentForgeId = equipmentForgeId;
            this.QualityLv = qualityLv;
            this.RarityFlags = rarityFlags;
            this.SlotType = slotType;
        }

        public EquipmentMB() : base(0L, false, "")
        {
        }
    }
}