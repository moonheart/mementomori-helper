using MementoMori.Ortega.Share.Data;
using MessagePack;

namespace MementoMori.User;

public class LoginPlayer
{
    public class Req
    {
        public string Password { get; set; }
        public long PlayerId { get; set; }
    }

    public class Resp
    {
        public string AuthTokenOfMagicOnion { get; set; }
        public object BanChatInfo { get; set; }
        public UserSyncData UserSyncData { get; set; }
    }

}