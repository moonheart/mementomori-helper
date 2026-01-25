using Quartz;
using MementoMori.Api.Services;
using MementoMori.Api.Infrastructure;

namespace MementoMori.Api.Jobs;

/// <summary>
/// 自动抽取圣装任务
/// </summary>
public class AutoDrawGachaRelicJob : AccountJobBase
{
    private readonly GameActionService _gameActionService;

    public AutoDrawGachaRelicJob(
        AccountManager accountManager,
        ILogger<AutoDrawGachaRelicJob> logger,
        IServiceProvider serviceProvider,
        GameActionService gameActionService)
        : base(accountManager, logger, serviceProvider)
    {
        _gameActionService = gameActionService;
    }

    protected override async Task ExecuteAsync(IJobExecutionContext context, AccountContext accountContext)
    {
        var userId = accountContext.AccountInfo.UserId;
        Logger.LogInformation("Starting AutoDrawGachaRelicJob for user {UserId}", userId);
        
        await _gameActionService.AutoDrawGachaRelicAsync(userId);
    }
}
