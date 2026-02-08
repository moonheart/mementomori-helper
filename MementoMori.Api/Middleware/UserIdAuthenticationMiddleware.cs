using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MementoMori.Api.Services;

namespace MementoMori.Api.Middleware;

public class UserIdAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserIdAuthenticationMiddleware> _logger;
    private readonly AccountManager _accountManager;
    
    // Endpoints that don't require user ID authentication
    private static readonly string[] ExcludedPaths = new[]
    {
        "/api/Auth/accounts",
        "/swagger",
        "/api/localization",
        "/health",
        "/api/master"
    };

    public UserIdAuthenticationMiddleware(
        RequestDelegate next,
        ILogger<UserIdAuthenticationMiddleware> logger, AccountManager accountManager)
    {
        _next = next;
        _logger = logger;
        _accountManager = accountManager;
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

        // Extract user ID from header or query string (for SignalR)
        string? userIdHeader = context.Request.Headers["X-User-Id"];
        
        if (string.IsNullOrEmpty(userIdHeader) && context.Request.Query.TryGetValue("userId", out var queryUserId))
        {
            userIdHeader = queryUserId;
        }

        if (string.IsNullOrWhiteSpace(userIdHeader))
        {
            _logger.LogWarning("Request to {Path} missing User ID", path);
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

        var accountContext = await _accountManager.GetOrCreateAsync(userId);

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
