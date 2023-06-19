using MementoMori.Ortega.Share.Data.Present;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserPresentDtoInfo
    {
        public long CreateAt { get; set; }

        public long DisplayLimitDate { get; set; }

        public string Guid { get; set; }

        public bool IsReceived { get; set; }

        public List<PresentItem> ItemList { get; set; }

        public string Message { get; set; }

        public long ReceiveLimitDate { get; set; }

        public string Title { get; set; }

        public bool IsTimeout(long timeStamp)
        {
            return this.DisplayLimitDate <= timeStamp;
        }

        public UserPresentDtoInfo()
        {
        }
    }
}