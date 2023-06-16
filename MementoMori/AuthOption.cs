namespace MementoMori;

public class AuthOption
{
    public string ClientKey { get; set; }
    public string DeviceToken { get; set; }
    public string AppVersion { get; set; }
    public string OSVersion { get; set; }
    public string ModelName { get; set; }
    public string AdverisementId { get; set; }
    public long UserId { get; set; }

    public Dictionary<string, string> Headers { get; set; }
}