using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Friend;

[MessagePackObject(true)]
[OrtegaApi("friend/getPlayerInfoList")]
public class GetPlayerInfoListRequest : ApiRequestBase
{
    public FriendInfoType FriendInfoType { get; set; }

    public LanguageType LanguageType { get; set; }
}