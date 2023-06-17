using MessagePack;

namespace MementoMori.Ortega.ServerLib.Models.MySql.Dto
{
    [MessagePackObject(true)]
    public class UserShopFirstChargeBonusDtoInfo
    {
        public bool IsReceivedDay1 { get; set; }

        public bool IsReceivedDay2 { get; set; }

        public bool IsReceivedDay3 { get; set; }

        public long OpenTimeStamp { get; set; }

        public UserShopFirstChargeBonusDtoInfo()
        {
        }
    }
}