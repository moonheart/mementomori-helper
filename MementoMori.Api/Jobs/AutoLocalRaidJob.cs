using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 自动时空洞窟任务
/// </summary>
public class AutoLocalRaidJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public AutoLocalRaidJob(
        AccountManager accountManager,
        ILogger<AutoLocalRaidJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Starting AutoLocalRaidJob for user {UserId}", userId);
        
        await _gameActionService.AutoLocalRaidAsync(userId);
    }
}
