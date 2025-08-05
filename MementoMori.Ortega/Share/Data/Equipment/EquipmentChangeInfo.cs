using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
    [Description("武具変更情報")]
    [MessagePackObject(true)]
    public class EquipmentChangeInfo
    {
        public string EquipmentGuid { get; set; }

        public long EquipmentId { get; set; }

        public EquipmentSlotType EquipmentSlotType { get; set; }

        public bool IsInherit { get; set; }

        public List<EquipmentInheritanceType> EquipmentInheritanceTypeList { get; set; }
    }
}