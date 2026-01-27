using MementoMori.Api.Infrastructure;
using MementoMori.Api.Handlers;
using MementoMori.Api.Handlers.LocalRaid;

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
            _serviceProvider.GetRequiredService<AutoBattleRewardHandler>(),
            _serviceProvider.GetRequiredService<BountyQuestDispatchHandler>(),
            _serviceProvider.GetRequiredService<BountyQuestRewardHandler>(),
            _serviceProvider.GetRequiredService<ShopAutoBuyHandler>(),
            _serviceProvider.GetRequiredService<GachaRelicChangeHandler>(),
            _serviceProvider.GetRequiredService<GachaRelicDrawHandler>(),
            _serviceProvider.GetRequiredService<GuildRaidOpenHandler>(),
            _serviceProvider.GetRequiredService<LegendLeagueHandler>(),
            _serviceProvider.GetRequiredService<LocalRaidHandler>(),
            _serviceProvider.GetRequiredService<ArenaPvpHandler>(),
            _serviceProvider.GetRequiredService<MissionRewardHandler>()
            // Note: AutoBossChallengeHandler 不在此列表中，它是用户手动启停的任务
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
        
        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动更换圣装目标
    /// </summary>
    public async Task AutoSetGachaRelicAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<GachaRelicChangeHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动抽取圣装
    /// </summary>
    public async Task AutoDrawGachaRelicAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<GachaRelicDrawHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动开启公会副本
    /// </summary>
    public async Task OpenGuildRaidAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<GuildRaidOpenHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动传奇竞技场对战
    /// </summary>
    public async Task AutoLegendLeagueAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<LegendLeagueHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动时空洞窟
    /// </summary>
    public async Task AutoLocalRaidAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<LocalRaidHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 仅执行自动竞技场对战
    /// </summary>
    public async Task AutoArenaPvpAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<ArenaPvpHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 领取所有任务奖励
    /// </summary>
    public async Task ClaimMissionRewardsAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<MissionRewardHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 领取自动战斗奖励
    /// </summary>
    public async Task GetAutoBattleRewardAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<AutoBattleRewardHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 执行BOSS快速战斗
    /// </summary>
    public async Task BossQuickBattleAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<BossQuickBattleHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 执行BOSS高速战斗
    /// </summary>
    public async Task BossHighSpeedBattleAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<BossHighSpeedBattleHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }

    /// <summary>
    /// 执行自动BOSS挑战
    /// </summary>
    public async Task AutoBossChallengeAsync(long userId)
    {
        var context = await _accountManager.GetOrCreateAsync(userId);
        var handler = _serviceProvider.GetRequiredService<AutoBossChallengeHandler>();

        await _executor.ExecuteActionsAsync(context, [handler]);
    }
}
