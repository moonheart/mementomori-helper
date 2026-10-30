using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 竞技场自动对战任务 (包含任务奖励领取)
/// </summary>
public class PvpJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public PvpJob(
        AccountManager accountManager,
        ILogger<PvpJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Starting PvpJob for user {UserId}", userId);
        
        // 1. 执行竞技场对战
        await _gameActionService.AutoArenaPvpAsync(userId);

        // 2. 领取任务奖励 (模拟原逻辑中的 CompleteMissions / RewardMissonActivity)
        await _gameActionService.ClaimMissionRewardsAsync(userId);
    }
}
