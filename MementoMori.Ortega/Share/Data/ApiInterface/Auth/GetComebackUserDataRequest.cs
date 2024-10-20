using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth;

[MessagePackObject(true)]
[OrtegaAuth("auth/getComebackUserData", false, true)]
public class GetComebackUserDataRequest : ApiRequestBase
{
    public string AppleIdToken { get; set; }
    public long FromUserId { get; set; }

    public string GoogleAuthorizationCode { get; set; }

    public string Password { get; set; }

    public SnsType SnsType { get; set; }

    public string TwitterAccessToken { get; set; }

    public string TwitterAccessTokenSecret { get; set; }

    public long UserId { get; set; }

    public int AuthToken { get; set; }
}