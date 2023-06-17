using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserNotificationDtoInfo
    {
        public NotificationType NotificationType { get; set; }

        public long Value { get; set; }

        public UserNotificationDtoInfo()
        {
        }
    }
}