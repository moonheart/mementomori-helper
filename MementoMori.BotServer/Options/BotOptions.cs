namespace MementoMori.BotServer.Options;

public class BotOptions
{
    public string BaseUri { get; set; }
    public string MentemoriIcuUri { get; set; }
    public List<long> AdminIds { get; set; } = new();
    public List<long> OpenedGroups { get; set; } = new();
    public List<long> LastNotices { get; set; } = new();
    public List<long> LastEvents { get; set; } = new();
}