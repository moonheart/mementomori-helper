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
        [PropertyOrder(10)]
		[Description("付加パラメータ合計値")]
		public long AdditionalParameterTotal { get; }

		[PropertyOrder(15)]
		[Description("進化先武具ID")]
		public long AfterLevelEvolutionEquipmentId { get; }

		[PropertyOrder(16)]
		[Description("進化先レアリティ武具ID")]
		public long AfterRarityEvolutionEquipmentId { get; }

		[Nest(false, 0)]
		[PropertyOrder(9)]
		[Description("基礎パラメータ")]
		public BattleParameterChangeInfo BattleParameterChangeInfo { get; }

		[PropertyOrder(7)]
		[Description("武具カテゴリ")]
		public EquipmentCategory Category { get; }

		[PropertyOrder(18)]
		[Description("合成情報ID")]
		public long CompositeId { get; }

		[PropertyOrder(17)]
		[Description("武具進化テーブルID")]
		public long EquipmentEvolutionId { get; }

		[PropertyOrder(14)]
		[Description("専属スキル説明文ID")]
		public long EquipmentExclusiveSkillDescriptionId { get; }

		[PropertyOrder(19)]
		[Description("鋳造ID")]
		public long EquipmentForgeId { get; }

		[PropertyOrder(3)]
		[Description("武具レベル")]
		public long EquipmentLv { get; }

		[PropertyOrder(12)]
		[Description("武具セットID")]
		public long EquipmentSetId { get; }

		[PropertyOrder(6)]
		[Description("装備可能キャラタイプ")]
		public JobFlags EquippedJobFlags { get; }

		[PropertyOrder(13)]
		[Description("専属効果ID")]
		public long ExclusiveEffectId { get; }

		[PropertyOrder(21)]
		[Description("最初の宝石スロット解放に必要な銅貨")]
		public long GoldRequiredToOpeningFirstSphereSlot { get; }

		[PropertyOrder(20)]
		[Description("ロック無し鍛錬に必要な銅貨")]
		public long GoldRequiredToTraining { get; }

		[PropertyOrder(22)]
		[Description("アイコンID")]
		public long IconId { get; }

		[PropertyOrder(1)]
		[Description("名称キー")]
		public string NameKey { get; }

		[PropertyOrder(4)]
		[Description("装備評価")]
		public long PerformancePoint { get; }

		[PropertyOrder(8)]
		[Description("品質レベル")]
		public int QualityLv { get; }

		[PropertyOrder(2)]
		[Description("レアリティ")]
		public EquipmentRarityFlags RarityFlags { get; }

		[PropertyOrder(5)]
		[Description("武具の装備枠タイプ")]
		public EquipmentSlotType SlotType { get; }

		[SerializationConstructor]
		public EquipmentMB(long id, bool? isIgnore, string memo, long additionalParameterTotal, long afterLevelEvolutionEquipmentId, long afterRarityEvolutionEquipmentId, BattleParameterChangeInfo battleParameterChangeInfo, EquipmentCategory category, long compositeId, long equipmentEvolutionId, long equipmentExclusiveSkillDescriptionId, long equipmentLv, long equipmentSetId, JobFlags equippedJobFlags, long exclusiveEffectId, long goldRequiredToOpeningFirstSphereSlot, long goldRequiredToTraining, long iconId, string nameKey, long performancePoint, long equipmentForgeId, int qualityLv, EquipmentRarityFlags rarityFlags, EquipmentSlotType slotType)
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

		public EquipmentMB()
			: base(0L, null, null)
		{
		}
    }
}