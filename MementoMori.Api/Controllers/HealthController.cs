using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Health check endpoint
    /// </summary>
    [HttpGet]
    public ActionResult<HealthResponse> Get()
    {
        return Ok(new HealthResponse
        {
            Status = "healthy",
            Version = "1.0.0",
            Timestamp = DateTime.UtcNow
        });
    }
}

[ExportTsClass]
public class HealthResponse
{
    public string Status { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
