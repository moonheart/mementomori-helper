using MementoMori.Ortega.Share.Data.Friend;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend;

[MessagePackObject(true)]
public class GetFriendStatusResponse : ApiResponseBase
{
    public List<FriendStatusInfo> FriendStatusInfoList { get; set; }
}
