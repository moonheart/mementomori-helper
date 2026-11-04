using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Models;

[ExportTsClass]
public class WorldDto
{
    public long WorldId { get; set; }
    public string WorldName { get; set; } = string.Empty;
    public long LastLoginTime { get; set; }
}
