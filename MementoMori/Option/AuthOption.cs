namespace MementoMori;

public class AuthOption
{
    [Obsolete]
    public string ClientKey { get; set; }

    public string AuthUrl { get; set; }
    public string DeviceToken { get; set; }
    public string AppVersion { get; set; }
    public string OSVersion { get; set; }
    public string ModelName { get; set; }

    [Obsolete]
    public long UserId { get; set; }

    public long LastLoginUserId { get; set; }

    public List<AccountInfo> Accounts { get; set; } = new();
}