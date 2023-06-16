using MessagePack;

namespace MementoMori;

public class GetServerHost
{
    public class Req
    {
        public int WorldId { get; set; }
    }

    public class Resp
    {
        public string ApiHost { get; set; }
        public string MagicOnionHost { get; set; }
        public int MagicOnionPort { get; set; }
    }
}