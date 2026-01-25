using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 动作执行协调器
/// </summary>
public class ActionExecutor
{
    private readonly ILogger<ActionExecutor> _logger;
    private readonly JobLogger _jobLogger;

    public ActionExecutor(ILogger<ActionExecutor> logger, JobLogger jobLogger)
    {
        _logger = logger;
        _jobLogger = jobLogger;
    }

    /// <summary>
    /// 按顺序执行一系列动作
    /// </summary>
    public async Task ExecuteActionsAsync(AccountContext context, IEnumerable<IGameActionHandler> handlers)
    {
        var userId = context.AccountInfo.UserId;
        
        foreach (var handler in handlers)
        {
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
                await _jobLogger.LogAsync(userId, $"{handler.ActionName} 执行失败: {ex.Message}", "Error");
            }
        }
    }
}
