using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item.Model
{
    [MessagePackObject(true)]
    public class UserEquipment : IUserItem, IReadOnlyEquipment, IAdditionalParameter
    {
        public string CharacterGuid { get; set; }

        public bool HasParameter { get; set; }

        public UserEquipment(long itemId, bool hasParameter = false)
        {
            this.ItemType = ItemType.Equipment;
            this.ItemId = itemId;
            this.ItemCount = 1;
            this.HasParameter = hasParameter;
            if (hasParameter)
            {
                string text = StringUtil.CreateGuid();
                this.Guid = text;
            }
        }

        public UserEquipment(long itemId, long itemCount)
        {
            this.ItemType = ItemType.Equipment;
            this.ItemId = itemId;
            this.ItemCount = itemCount;
        }

        public UserEquipment(long itemId, string guid)
        {
            this.ItemType = ItemType.Equipment;
            this.ItemId = itemId;
            this.Guid = guid;
        }

        public UserEquipment(UserEquipmentDtoInfo userEquipmentDtoInfo)
        {
            this.ItemType = (ItemType) ((ulong) 4L);
            long EquipmentId = userEquipmentDtoInfo.EquipmentId;
            this.ItemId = EquipmentId;
            this.ItemCount = (long) ((ulong) 1L);
            this.HasParameter = true;
            string text = userEquipmentDtoInfo.Guid;
            this.Guid = text;
            string text2 = userEquipmentDtoInfo.CharacterGuid;
            this.CharacterGuid = text2;
            long num = userEquipmentDtoInfo.AdditionalParameterHealth;
            this.AdditionalParameterHealth = num;
            long num2 = userEquipmentDtoInfo.AdditionalParameterMuscle;
            this.AdditionalParameterMuscle = num2;
            long num3 = userEquipmentDtoInfo.AdditionalParameterEnergy;
            this.AdditionalParameterEnergy = num3;
            long num4 = userEquipmentDtoInfo.AdditionalParameterIntelligence;
            this.AdditionalParameterIntelligence = num4;
            long num5 = userEquipmentDtoInfo.SphereUnlockedCount;
            this.SphereUnlockedCount = num5;
            long num6 = userEquipmentDtoInfo.SphereId1;
            this.SphereId1 = num6;
            long num7 = userEquipmentDtoInfo.SphereId2;
            this.SphereId2 = num7;
            long num8 = userEquipmentDtoInfo.SphereId3;
            this.SphereId3 = num8;
            long num9 = userEquipmentDtoInfo.SphereId4;
            this.SphereId4 = num9;
            long num10 = userEquipmentDtoInfo.LegendSacredTreasureExp;
            this.LegendSacredTreasureExp = num10;
            long num11 = userEquipmentDtoInfo.LegendSacredTreasureLv;
            this.LegendSacredTreasureLv = num11;
            long num12 = userEquipmentDtoInfo.MatchlessSacredTreasureExp;
            this.MatchlessSacredTreasureExp = num12;
            long num13 = userEquipmentDtoInfo.MatchlessSacredTreasureLv;
            this.MatchlessSacredTreasureLv = num13;
            long num14 = userEquipmentDtoInfo.ReinforcementLv;
            this.ReinforcementLv = num14;
            long num15 = userEquipmentDtoInfo.BeforeSynchroEquipmentId;
            this.BeforeSynchroEquipmentId = num15;
            long num16 = userEquipmentDtoInfo.SetBaseSynchroGroupId;
            this.SetBaseSynchroGroupId = num16;
            long num17 = userEquipmentDtoInfo.PlayerId;
            this.PlayerId = num17;
        }

        [SerializationConstructor]
        [JsonConstructor]
        public UserEquipment(long itemId, long itemCount, bool hasParameter, string guid, string characterGuid, long additionalParameterHealth, long additionalParameterMuscle,
            long additionalParameterEnergy, long additionalParameterIntelligence, long sphereUnlockedCount, long sphereId1, long sphereId2, long sphereId3, long sphereId4,
            long legendSacredTreasureExp, long legendSacredTreasureLv, long matchlessSacredTreasureExp, long matchlessSacredTreasureLv, long reinforcementLv, long beforeSynchroEquipmentId, long setBaseSynchroGroupId)
        {
            this.ItemType = (ItemType) ((ulong) 4L);
            this.ItemId = itemId;
            this.ItemCount = itemCount;
            this.HasParameter = hasParameter;
            this.Guid = guid;
            this.CharacterGuid = characterGuid;
            this.AdditionalParameterHealth = additionalParameterHealth;
            this.AdditionalParameterMuscle = additionalParameterMuscle;
            this.AdditionalParameterEnergy = additionalParameterEnergy;
            this.AdditionalParameterIntelligence = additionalParameterIntelligence;
            this.SphereUnlockedCount = sphereUnlockedCount;
            this.SphereId1 = sphereId1;
            this.SphereId2 = sphereId2;
            this.SphereId3 = sphereId3;
            this.SphereId4 = sphereId4;
            this.LegendSacredTreasureExp = legendSacredTreasureExp;
            this.LegendSacredTreasureLv = legendSacredTreasureLv;
            this.MatchlessSacredTreasureExp = matchlessSacredTreasureExp;
            this.MatchlessSacredTreasureLv = matchlessSacredTreasureLv;
            this.ReinforcementLv = reinforcementLv;
            this.BeforeSynchroEquipmentId = beforeSynchroEquipmentId;
            this.SetBaseSynchroGroupId = setBaseSynchroGroupId;
        }

        public long EquipmentId
        {
            get { return this.ItemId; }
        }

        public string Guid { get; }

        public long ItemCount { get; }

        public long ItemId { get; }

        public ItemType ItemType { get; }

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

        public long AdditionalParameterHealth { get; set; }

        public long AdditionalParameterIntelligence { get; set; }

        public long AdditionalParameterMuscle { get; set; }

        public long AdditionalParameterEnergy { get; set; }

        public long SphereId1 { get; set; }

        public long SphereId2 { get; set; }

        public long SphereId3 { get; set; }

        public long SphereId4 { get; set; }

        public long SphereUnlockedCount { get; set; }

        public long LegendSacredTreasureExp { get; set; }

        public long LegendSacredTreasureLv { get; set; }

        public long MatchlessSacredTreasureExp { get; set; }

        public long MatchlessSacredTreasureLv { get; set; }

        public long ReinforcementLv { get; set; }

        public long BeforeSynchroEquipmentId { get; set; }

        public long SetBaseSynchroGroupId { get; set; }

        public long PlayerId { get; set; }
    }
}