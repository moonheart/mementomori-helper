using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserVipGiftDtoInfo
    {
        public long PlayerId { get; set; }

        public long VipGiftId { get; set; }

        public long VipLv { get; set; }

        public UserVipGiftDtoInfo()
        {
        }
    }
}