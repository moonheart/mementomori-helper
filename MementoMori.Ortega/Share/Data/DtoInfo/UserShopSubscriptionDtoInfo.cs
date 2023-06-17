using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserShopSubscriptionDtoInfo
    {
        public string ProductId { get; set; }

        public DeviceType DeviceType { get; set; }

        public string TransactionId { get; set; }

        public long ExpirationTimeStamp { get; set; }

        public UserShopSubscriptionDtoInfo()
        {
        }
    }
}