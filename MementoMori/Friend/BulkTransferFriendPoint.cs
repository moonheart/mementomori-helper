using MementoMori.Ortega.Share.Data;
using MessagePack;

namespace MementoMori.Friend;

public class BulkTransferFriendPoint
{
    public class Req
    {
    }

    public class Resp
    {
        public UserSyncData UserSyncData { get; set; }
    }
}