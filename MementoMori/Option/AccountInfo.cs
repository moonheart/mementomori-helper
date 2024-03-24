namespace MementoMori;

public class AccountInfo
{
    public string Name { get; set; }
    public long UserId { get; set; }
    public string ClientKey { get; set; }
    public bool AutoLogin { get; set; }
    public long AutoLoginWorldId { get; set; }
}