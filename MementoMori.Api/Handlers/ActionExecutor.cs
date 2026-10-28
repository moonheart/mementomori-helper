using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 动作执行协调器
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class ActionExecutor
{
    private readonly ILogger<ActionExecutor> _logger;
    private readonly JobLogger _jobLogger;

    /// <summary>
    /// 按顺序执行一系列动作
    /// </summary>
    public async Task ExecuteActionsAsync(AccountContext context, IEnumerable<IGameActionHandler> handlers)
    {
        var userId = context.AccountInfo.UserId;
        
        foreach (var handler in handlers)
        {
            // 会话丢了 (别处登录) 就别继续刷错误, 下次任务会重新登录
            if (!context.AccountInfo.IsLoggedIn)
            {
                await _jobLogger.LogAsync(userId, "Session lost (logged in on another device?), stopping this run. Next run logs back in.", "Warning");
                break;
            }

            try
            {
                _logger.LogInformation("Starting action {ActionName} for user {UserId}", handler.ActionName, userId);
                // await _jobLogger.LogAsync(userId, $"开始执行: {handler.ActionName}");
                
                await handler.ExecuteAsync(context);
                
                // 动作之间的微小延迟，模拟真实操作节奏
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Action {ActionName} failed for user {UserId}", handler.ActionName, userId);
                await _jobLogger.LogAsync(userId, Text.T("JobActionFailed", handler.ActionName, ex.Message), "Error");
            }
        }
    }
}
