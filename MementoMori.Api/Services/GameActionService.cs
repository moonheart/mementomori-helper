using MementoMori.Api.Infrastructure;
using MementoMori.Api.Handlers;

namespace MementoMori.Api.Services;

/// <summary>
/// 游戏业务动作服务 - 协调各种 Handler 的执行
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class GameActionService
{
    private readonly ILogger<GameActionService> _logger;
    private readonly AccountManager _accountManager;
    private readonly JobLogger _jobLogger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ActionExecutor _executor;

    /// <summary>
    /// 执行所有快速动作（每日/每小时任务的核心）
    /// </summary>
    public async Task ExecuteAllQuickActionAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        await _jobLogger.LogAsync(userId, "开始执行全量自动化任务...");
        
        var handlers = new List<IGameActionHandler>
        {
            _serviceProvider.GetRequiredService<DailyLoginBonusHandler>(),
            _serviceProvider.GetRequiredService<ShopAutoBuyHandler>()
        };

        await _executor.ExecuteActionsAsync(context, handlers);
        
        await _jobLogger.LogAsync(userId, "全量自动化任务执行完毕。");
    }

    /// <summary>
    /// 仅执行自动购买商店
    /// </summary>
    public async Task AutoBuyShopItemAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<ShopAutoBuyHandler>();
        
        await _executor.ExecuteActionsAsync(context, new[] { handler });
    }
}
