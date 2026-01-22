using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MementoMori.Api.Middleware;

public class UserIdAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserIdAuthenticationMiddleware> _logger;
    
    // Endpoints that don't require user ID authentication
    private static readonly string[] ExcludedPaths = new[]
    {
        "/api/Auth/accounts",
        "/swagger",
        "/health"
    };

    public UserIdAuthenticationMiddleware(
        RequestDelegate next,
        ILogger<UserIdAuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value ?? string.Empty;

        // Skip authentication for excluded paths
        if (ShouldSkipAuthentication(path))
        {
            await _next(context);
            return;
        }

        // Extract user ID from header
        if (!context.Request.Headers.TryGetValue("X-User-Id", out var userIdHeader) ||
            string.IsNullOrWhiteSpace(userIdHeader))
        {
            _logger.LogWarning("Request to {Path} missing X-User-Id header", path);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unauthorized",
                message = "X-User-Id header is required"
            });
            return;
        }

        // Validate user ID format
        if (!long.TryParse(userIdHeader, out var userId))
        {
            _logger.LogWarning("Invalid X-User-Id header value: {UserIdHeader}", userIdHeader);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Unauthorized",
                message = "Invalid X-User-Id format"
            });
            return;
        }

        // Store user ID in HttpContext for use in controllers
        context.Items["UserId"] = userId;

        await _next(context);
    }

    private static bool ShouldSkipAuthentication(string path)
    {
        foreach (var excludedPath in ExcludedPaths)
        {
            if (path.StartsWith(excludedPath, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
