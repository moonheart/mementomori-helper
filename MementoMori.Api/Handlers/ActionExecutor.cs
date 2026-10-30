using MementoMori.Ortega.Share.Enums;
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
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 按顺序执行一系列动作
    /// </summary>
    public async Task ExecuteActionsAsync(AccountContext context, IEnumerable<IGameActionHandler> handlers)
    {
        var userId = context.AccountInfo.UserId;
        var relogged = false;

        foreach (var handler in handlers)
        {
            if (!context.AccountInfo.IsLoggedIn)
            {
                // 没人在别处登录 (只是 NoSession, 比如游戏服务器出错把会话弄没了): 马上重新登录一次, 接着跑
                if (!relogged && context.NetworkManager.LastSessionError == ErrorCode.CommonNoSession && await ReloginAsync(context))
                {
                    relogged = true;
                }
                else
                {
                    // 有人在别处登录 (玩家在玩) 就别抢, 下次任务会重新登录
                    await _jobLogger.LogAsync(userId, "Session lost (logged in on another device?), stopping this run. Next run logs back in.", "Warning");
                    break;
                }
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

    private async Task<bool> ReloginAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var resp = await scope.ServiceProvider.GetRequiredService<AccountService>().LoginAsync(userId, context.AccountInfo.ClientKey);
            await _jobLogger.LogAsync(userId, resp.Success ? "Session expired on the game server, logged back in." : $"Re-login failed: {resp.Message}", resp.Success ? "Info" : "Warning");
            return resp.Success;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Re-login failed for user {UserId}", userId);
            return false;
        }
    }
}
