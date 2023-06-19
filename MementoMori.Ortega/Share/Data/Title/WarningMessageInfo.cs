using MessagePack;

namespace MementoMori.Ortega.Share.Data.Title
{
    [MessagePackObject(true)]
    public class WarningMessageInfo
    {
        public DateTime ConfirmDateTime { get; set; }

        public int DisplayOrder { get; set; }

        public AccountMessageInfo MessageInfo { get; set; }

        public long WarningId { get; set; }

        public long WorldId { get; set; }

        public WarningMessageInfo()
        {
        }
    }
}