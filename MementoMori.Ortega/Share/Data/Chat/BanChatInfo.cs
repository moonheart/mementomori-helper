using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
    [MessagePackObject(true)]
    public class BanChatInfo
    {
        public AccountSuspensionType AccountSuspensionType { get; set; }

        public BanChatType BanChatType { get; set; }

        public string LiftDateTime { get; set; }

        public BanChatInfo()
        {
        }
    }
}