using MessagePack;

namespace MementoMori.Ortega.Share.Data.Title
{
    [MessagePackObject(true)]
    public class AccountMessageInfo
    {
        public string Message { get; set; }

        public long PlayerId { get; set; }

        public string Title { get; set; }

        public bool IsTarget(long playerId)
        {
            return this.PlayerId == playerId;
        }

        public AccountMessageInfo()
        {
        }
    }
}