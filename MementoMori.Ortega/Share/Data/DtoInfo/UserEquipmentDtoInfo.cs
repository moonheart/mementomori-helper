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
            CreateAt = userEquipmentDtoInfo.CreateAt;
            PlayerId = userEquipmentDtoInfo.PlayerId;
            AdditionalParameterIntelligence = userEquipmentDtoInfo.AdditionalParameterIntelligence;
            AdditionalParameterHealth = userEquipmentDtoInfo.AdditionalParameterHealth;
            AdditionalParameterMuscle = userEquipmentDtoInfo.AdditionalParameterMuscle;
            AdditionalParameterEnergy = userEquipmentDtoInfo.AdditionalParameterEnergy;
            EquipmentId = userEquipmentDtoInfo.EquipmentId;
            Guid = userEquipmentDtoInfo.Guid;
            CharacterGuid = userEquipmentDtoInfo.CharacterGuid;
            SphereId1 = userEquipmentDtoInfo.SphereId1;
            SphereId2 = userEquipmentDtoInfo.SphereId2;
            SphereId3 = userEquipmentDtoInfo.SphereId3;
            SphereId4 = userEquipmentDtoInfo.SphereId4;
            SphereUnlockedCount = userEquipmentDtoInfo.SphereUnlockedCount;
            LegendSacredTreasureExp = userEquipmentDtoInfo.LegendSacredTreasureExp;
            LegendSacredTreasureLv = userEquipmentDtoInfo.LegendSacredTreasureLv;
            MatchlessSacredTreasureExp = userEquipmentDtoInfo.MatchlessSacredTreasureExp;
            MatchlessSacredTreasureLv = userEquipmentDtoInfo.MatchlessSacredTreasureLv;
            ReinforcementLv = userEquipmentDtoInfo.ReinforcementLv;
            BeforeSynchroEquipmentId = userEquipmentDtoInfo.BeforeSynchroEquipmentId;
            SetBaseSynchroGroupId = userEquipmentDtoInfo.SetBaseSynchroGroupId;
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

        [Description("シンクロ反映前の武具ID")]
        public long BeforeSynchroEquipmentId { get; set; }

        [Description("ベース枠に設定中のシンクログループID")]
        public long SetBaseSynchroGroupId { get; set; }

        public long[] GetSphereIds()
        {
            return new[]
            {
                SphereId1, SphereId2, SphereId3, SphereId4
            };
        }

        public long GetAdditionalParameter(BaseParameterType baseParameterType)
        {
            return baseParameterType switch
            {
                BaseParameterType.Energy => AdditionalParameterEnergy,
                BaseParameterType.Muscle => AdditionalParameterMuscle,
                BaseParameterType.Health => AdditionalParameterHealth,
                BaseParameterType.Intelligence => AdditionalParameterIntelligence,
                _ => 0
            };
        }

        public void SetEquipmentMB(EquipmentMB equipmentMB)
        {
            if (equipmentMB == null)
            {
                return;
            }
            EquipmentId = equipmentMB.Id;
            var value = equipmentMB.AdditionalParameterTotal / 4;
            AdditionalParameterHealth = value;
            AdditionalParameterIntelligence = value;
            AdditionalParameterMuscle = value;
            AdditionalParameterEnergy = value;
        }
    }
}