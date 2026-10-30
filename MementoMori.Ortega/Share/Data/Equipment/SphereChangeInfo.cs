using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
    [MessagePackObject(true)]
    public class SphereChangeInfo
    {
        public string EquipmentGuid { get; set; }

        public long SphereId1 { get; set; }

        public long SphereId2 { get; set; }

        public long SphereId3 { get; set; }

        public long SphereId4 { get; set; }
    }
}
