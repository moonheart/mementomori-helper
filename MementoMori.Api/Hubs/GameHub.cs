using Microsoft.AspNetCore.SignalR;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Hubs;

public class GameHub : Hub
{
    /// <summary>
    /// Client subscribes to account updates
    /// </summary>
    public async Task SubscribeToAccount(long accountId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"account_{accountId}");
        await Clients.Caller.SendAsync("Subscribed", accountId);
    }

    /// <summary>
    /// Client unsubscribes from account updates
    /// </summary>
    public async Task UnsubscribeFromAccount(long accountId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"account_{accountId}");
    }

    /// <summary>
    /// Send task update to all clients subscribed to this account
    /// </summary>
    public async Task SendTaskUpdate(long accountId, TaskUpdateMessage message)
    {
        await Clients.Group($"account_{accountId}").SendAsync("ReceiveTaskUpdate", message);
    }
}

[ExportTsClass]
public class TaskUpdateMessage
{
    public string TaskName { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
    public object? Data { get; set; }
}

[ExportTsEnum]
public enum TaskStatus
{
    Pending,
    Running,
    Completed,
    Failed
}
