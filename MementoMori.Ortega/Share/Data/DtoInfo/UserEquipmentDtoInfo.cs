using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserEquipmentDtoInfo : IReadOnlyEquipment
    {
        [Description("キャラクター固有ID")]
        public string CharacterGuid { get; set; }

        [Description("作成時刻")]
        public long CreateAt { get; set; }

        [Description("プレイヤーID")]
        public long PlayerId { get; set; }

        public UserEquipmentDtoInfo()
        {
        }

        public UserEquipmentDtoInfo(UserEquipmentDtoInfo userEquipmentDtoInfo)
        {
            this.CreateAt = userEquipmentDtoInfo.CreateAt;
            this.PlayerId = userEquipmentDtoInfo.PlayerId;
            this.AdditionalParameterIntelligence = userEquipmentDtoInfo.AdditionalParameterIntelligence;
            this.AdditionalParameterHealth = userEquipmentDtoInfo.AdditionalParameterHealth;
            this.AdditionalParameterMuscle = userEquipmentDtoInfo.AdditionalParameterMuscle;
            this.AdditionalParameterEnergy = userEquipmentDtoInfo.AdditionalParameterEnergy;
            this.EquipmentId = userEquipmentDtoInfo.EquipmentId;
            this.Guid = userEquipmentDtoInfo.Guid;
            this.CharacterGuid = userEquipmentDtoInfo.CharacterGuid;
            this.SphereId1 = userEquipmentDtoInfo.SphereId1;
            this.SphereId2 = userEquipmentDtoInfo.SphereId2;
            this.SphereId3 = userEquipmentDtoInfo.SphereId3;
            this.SphereId4 = userEquipmentDtoInfo.SphereId4;
            this.SphereUnlockedCount = userEquipmentDtoInfo.SphereUnlockedCount;
            this.LegendSacredTreasureExp = userEquipmentDtoInfo.LegendSacredTreasureExp;
            this.LegendSacredTreasureLv = userEquipmentDtoInfo.LegendSacredTreasureLv;
            this.MatchlessSacredTreasureExp = userEquipmentDtoInfo.MatchlessSacredTreasureExp;
            this.MatchlessSacredTreasureLv = userEquipmentDtoInfo.MatchlessSacredTreasureLv;
            this.ReinforcementLv = userEquipmentDtoInfo.ReinforcementLv;
        }

        [Description("付与パラメータ(体力)")]
        public long AdditionalParameterHealth { get; set; }

        [Description("付与パラメータ(知力)")]
        public long AdditionalParameterIntelligence { get; set; }

        [Description("付与パラメータ(筋力)")]
        public long AdditionalParameterMuscle { get; set; }

        [Description("付与パラメータ(敏捷)")]
        public long AdditionalParameterEnergy { get; set; }

        [Description("武具ID")]
        public long EquipmentId { get; set; }

        [Description("固有ID")]
        public string Guid { get; set; }

        [Description("宝石ID1")]
        public long SphereId1 { get; set; }

        [Description("宝石ID2")]
        public long SphereId2 { get; set; }

        [Description("宝石ID3")]
        public long SphereId3 { get; set; }

        [Description("宝石ID4")]
        public long SphereId4 { get; set; }

        [Description("宝石スロット解放数")]
        public long SphereUnlockedCount { get; set; }

        [Description("聖装経験値")]
        public long LegendSacredTreasureExp { get; set; }

        [Description("聖装レベル")]
        public long LegendSacredTreasureLv { get; set; }

        [Description("魔装経験値")]
        public long MatchlessSacredTreasureExp { get; set; }

        [Description("魔装レベル")]
        public long MatchlessSacredTreasureLv { get; set; }

        [Description("強化レベル")]
        public long ReinforcementLv { get; set; }

        public long[] GetSphereIds()
        {
            long[] array = new long[4];
            long num = this.SphereId1;
            array[0] = num;
            long num2 = this.SphereId2;
            array[1] = num2;
            long num3 = this.SphereId3;
            array[2] = num3;
            long num4 = this.SphereId4;
            array[3] = num4;
            return array;
        }

        public long GetAdditionalParameter(BaseParameterType baseParameterType)
        {
            if (baseParameterType != BaseParameterType.Muscle)
            {
            }

            return this.AdditionalParameterHealth;
        }

        public void SetEquipmentMB(EquipmentMB equipmentMB)
        {
            this.EquipmentId = equipmentMB.Id;
            this.AdditionalParameterHealth += equipmentMB.AdditionalParameterTotal;
            this.AdditionalParameterIntelligence += equipmentMB.AdditionalParameterTotal;
            this.AdditionalParameterMuscle += equipmentMB.AdditionalParameterTotal;
            this.AdditionalParameterEnergy += equipmentMB.AdditionalParameterTotal;
        }
    }
}