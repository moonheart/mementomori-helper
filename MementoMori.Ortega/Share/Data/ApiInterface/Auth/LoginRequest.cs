using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Auth;

[MessagePackObject(true)]
[OrtegaAuth("auth/login", false, true)]
public class LoginRequest : ApiRequestBase
{
    public string ClientKey { get; set; }

    public string DeviceToken { get; set; }

    public string AppVersion { get; set; }

    public string OSVersion { get; set; }

    public string ModelName { get; set; }

    public string AdverisementId { get; set; }

    public long UserId { get; set; }

    public bool IsPushNotificationAllowed { get; set; }
}