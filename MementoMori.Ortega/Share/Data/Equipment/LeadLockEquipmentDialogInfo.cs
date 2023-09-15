using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
    [MessagePackObject(true)]
    public class LeadLockEquipmentDialogInfo
    {
        public LeadLockEquipmentDialogType DialogType { get; set; }

        public int PassedDays { get; set; }
    }
}