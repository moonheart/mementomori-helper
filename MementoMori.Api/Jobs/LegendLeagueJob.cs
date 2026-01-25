using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 自动传奇竞技场对战任务
/// </summary>
public class LegendLeagueJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public LegendLeagueJob(
        AccountManager accountManager,
        ILogger<LegendLeagueJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Starting LegendLeagueJob for user {UserId}", userId);
        
        await _gameActionService.AutoLegendLeagueAsync(userId);
    }
}
