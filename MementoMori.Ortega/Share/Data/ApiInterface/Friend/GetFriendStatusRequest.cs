using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend;

[MessagePackObject(true)]
[OrtegaApi("friend/getFriendStatus", true, false)]
public class GetFriendStatusRequest : ApiRequestBase
{
    public List<long> TargetPlayerIdList { get; set; }
}
