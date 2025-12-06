using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Equipment;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Equipment
{
    [OrtegaApi("equipment/changeEquipment", true, false)]
    [MessagePackObject(true)]
    public class ChangeEquipmentRequest : ApiRequestBase
    {
        public string UserCharacterGuid { get; set; }

        public List<EquipmentChangeInfo> EquipmentChangeInfos { get; set; }

        public bool IsBulk { get; set; }

        public int Code { get; set; }
    }
}