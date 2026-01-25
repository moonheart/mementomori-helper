using Microsoft.AspNetCore.SignalR;

namespace MementoMori.Api.Infrastructure;

/// <summary>
/// 任务日志 Hub - 用于推送定时任务执行状态
/// </summary>
public class JobHub : Hub
{
    private readonly ILogger<JobHub> _logger;

    public JobHub(ILogger<JobHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        // 客户端连接时，根据 UserId 加入对应的 Group
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            _logger.LogInformation("SignalR client {ConnectionId} joined group {UserId}", Context.ConnectionId, userId);
        }
        await base.OnConnectedAsync();
    }
}

/// <summary>
/// 任务日志推送器
/// </summary>
public class JobLogger
{
    private readonly IHubContext<JobHub> _hubContext;

    public JobLogger(IHubContext<JobHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task LogAsync(long userId, string message, string level = "Info")
    {
        await _hubContext.Clients.Group(userId.ToString()).SendAsync("ReceiveLog", new
        {
            Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Level = level,
            Message = message
        });
    }
}
