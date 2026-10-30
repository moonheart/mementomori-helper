using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

[ExportTsClass]
public class JobStatusDto
{
    public string JobType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? NextRunTime { get; set; }
    public DateTime? LastRunTime { get; set; }
    public bool IsActive { get; set; }
}

[ExportTsClass]
public class TriggerJobRequest
{
    public string JobType { get; set; } = string.Empty;
}
