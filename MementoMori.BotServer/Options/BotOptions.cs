namespace MementoMori.BotServer.Options;

public class BotOptions
{
    public string BaseUri { get; set; }
    public string MentemoriIcuUri { get; set; }
    public string LiteDbConnStr { get; set; }
    public List<long> AdminIds { get; set; } = new();
    public List<long> OpenedGroups { get; set; } = new();
    public List<long> LastNotices { get; set; } = new();
    public List<long> LastEvents { get; set; } = new();
    public long LastDmmGameId { get; set; }
    public string DmmApiUrl { get; set; }
    public string NoticeApiAuth { get; set; }
}