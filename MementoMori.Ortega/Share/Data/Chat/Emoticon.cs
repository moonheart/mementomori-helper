using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
    [MessagePackObject(true)]
    public class Emoticon
    {
        public long EmoticonId { get; set; }
    }
}